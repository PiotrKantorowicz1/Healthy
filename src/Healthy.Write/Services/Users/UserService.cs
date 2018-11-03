using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Core.Types;
using Healthy.Write.Services.Users.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Healthy.Write.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IOneTimeSecuredOperationService _securedOperationService;

        public UserService(IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IOneTimeSecuredOperationService securedOperationService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _securedOperationService = securedOperationService;
        }

        public async Task<bool> IsNameAvailableAsync(string name)
            => await _userRepository.ExistsAsync(name.ToLowerInvariant()) == false;

        public async Task<Maybe<User>> GetAsync(string userId)
            => await _userRepository.GetByUserIdAsync(userId);

        public async Task<Maybe<User>> GetByNameAsync(string name)
            => await _userRepository.GetByNameAsync(name);

        public async Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId)
            => await _userRepository.GetByExternalUserIdAsync(externalUserId);

        public async Task<Maybe<User>> GetByEmailAsync(string email, string provider)
            => await _userRepository.GetByEmailAsync(email, provider);

        public async Task<Maybe<string>> GetStateAsync(string userId)
            => await _userRepository.GetStateAsync(userId);

        public async Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query)
            => await _userRepository.BrowseAsync(query);

        public async Task SignUpAsync(string userId, string email, string role,
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
            if (provider == Providers.Healthy && password.Empty())
            {
                throw new ServiceException(ErrorCodes.InvalidPassword,
                    $"Password can not be empty!");

            }
            if (!Roles.IsValid(role))
            {
                throw new ServiceException(ErrorCodes.InvalidRole, 
                    $"Can not create a new account for user id: '{userId}', invalid role: '{role}'.");
            }
            if (role == Roles.Owner)
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
                user.Value.SetName(name);
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
        }

        public async Task ChangeNameAsync(string userId, string name)
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
            user.Value.SetName(name);
            user.Value.Activate();
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task ActivateAsync(string email, string token)
        {
            await _securedOperationService.ConsumeAsync(OneTimeSecuredOperations.ActivateAccount,
                email, token);
            var user = await _userRepository.GetByEmailAsync(email, Providers.Healthy);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }
            user.Value.Activate();
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task LockAsync(string userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            if (user.Role == Roles.Owner)
            {
                throw new ServiceException(ErrorCodes.OwnerCannotBeLocked,
                    $"Owner account: '{userId}' can not be locked.");
            }
            user.Lock();
            await _userRepository.UpdateAsync(user);
        }

        public async Task UnlockAsync(string userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            user.Unlock();
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(string userId, bool soft)
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