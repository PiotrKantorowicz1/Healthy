using System;
using System.Linq;
using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Enumerations;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Core.Types;
using Healthy.EventStore.EventsStore;
using Healthy.Infrastructure.Dispatchers;
using Healthy.Services.Services.Users.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Healthy.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IEventStore _eventStore;
        private readonly IOneTimeSecuredOperationService _securedOperationService;

        public UserService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IOneTimeSecuredOperationService securedOperationService, 
            IEventDispatcher eventDispatcher, 
            IEventStore eventStore)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _securedOperationService = securedOperationService;
            _eventDispatcher = eventDispatcher;
            _eventStore = eventStore;
        }

        public async Task<bool> IsNameAvailableAsync(string name)
            => await _userRepository.ExistsAsync(name.ToLowerInvariant()) == false;

        public async Task<Maybe<User>> GetAsync(Guid userId)
            => await _userRepository.GetByUserIdAsync(userId);

        public async Task<Maybe<User>> GetByNameAsync(string name)
            => await _userRepository.GetByNameAsync(name);

        public async Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId)
            => await _userRepository.GetByExternalUserIdAsync(externalUserId);

        public async Task<Maybe<User>> GetByEmailAsync(string email, string provider)
            => await _userRepository.GetByEmailAsync(email, provider);

        public async Task<Maybe<States>> GetStateAsync(Guid userId)
            => await _userRepository.GetStateAsync(userId);

        public async Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query)
            => await _userRepository.BrowseAsync(query);

        public async Task SignUpAsync(Guid id, Guid userId, string email, string role,
            string provider, string password = null, string externalUserId = null,
            bool activate = true, string name = null)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user.HasValue)
            {
                throw new ServiceException(ErrorCodes.UserIdInUse,
                    $"User with id: '{userId}' already exists.");
            }
            user = await _userRepository.GetByEmailAsync(email, provider);
            if (user.HasValue)
            {
                throw new ServiceException(ErrorCodes.EmailInUse,
                    $"User with email: {email} already exists!");
            }
            user = await _userRepository.GetByNameAsync(name);
            if (user.HasValue)
            {
                throw new ServiceException(ErrorCodes.NameInUse,
                    $"User with name: {name} already exists!");
            }
            if (provider == ProviderType.Healthy.Name && password.Empty())
            {
                throw new ServiceException(ErrorCodes.InvalidPassword,
                    "Password can not be empty!");

            }
            if (role == Roles.Owner.Name)
            {
                var owner = await _userRepository.GetOwnerAsync();
                if (owner.HasValue)
                {
                    throw new ServiceException(ErrorCodes.OwnerAlreadyExists, 
                        $"Can not create a new owner account for user id: '{userId}'.");                    
                }
            }
            user = new User(userId, email, role, provider);
            if (!password.Empty())
                user.Value.SetPassword(password, _passwordHasher);
            if (name.NotEmpty())
            {
                user.Value.SetName(userId, name, user.Value.State.Name);
                if (activate)
                    user.Value.Activate();
                else
                    user.Value.SetUnconfirmed();
            }
            if (externalUserId.NotEmpty())
            {
                user.Value.SetExternalUserId(externalUserId);
            }
            await _userRepository.AddAsync(user.Value);
            await _eventDispatcher.DispatchAsync(user.Value.Events.ToArray());
        }

        public async Task ChangeNameAsync(Guid userId, string name)
        {
            var user = await GetAsync(userId);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }
            if (await IsNameAvailableAsync(name) == false)
            {
                throw new ServiceException(ErrorCodes.NameInUse,
                    $"User with name: '{name}' already exists.");
            }
            user.Value.SetName(userId, name, user.Value.State.Name);
            user.Value.Activate();
            await _userRepository.UpdateAsync(user.Value);
            _eventStore.Store(user.Value);
            user.Value.ClearEvents();
        }

        public async Task ActivateAsync(string email, string token)
        {
            await _securedOperationService.ConsumeAsync(OneTimeSecuredOperationType.ActivateAccount.Name,
                email, token);
            var user = await _userRepository.GetByEmailAsync(email, ProviderType.Healthy.Name);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }
            user.Value.Activate();
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task LockAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            if (user.Role == Roles.Owner)
            {
                throw new ServiceException(ErrorCodes.OwnerCannotBeLocked,
                    $"Owner account: '{userId}' can not be locked.");
            }
            user.Lock();
            await _userRepository.UpdateAsync(user);
            _eventStore.Store(user);     
            user.ClearEvents();
        }

        public async Task UnlockAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            user.Unlock();
            await _userRepository.UpdateAsync(user);
            _eventStore.Store(user);
            user.ClearEvents();
        }

        public async Task DeleteAsync(Guid userId, bool soft)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            if(soft)
            {
                user.MarkAsDeleted();
                await _userRepository.UpdateAsync(user);

                return;
            }
            await _userRepository.DeleteAsync(userId);
        }
    }
}