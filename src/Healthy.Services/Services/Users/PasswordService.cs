using System;
using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Enumerations;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Services.Services.Users.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Healthy.Services.Services.Users
{
    public class PasswordService : IPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOneTimeSecuredOperationService _oneTimeSecuredOperationService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordService(IUserRepository userRepository,
            IOneTimeSecuredOperationService oneTimeSecuredOperationService,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _oneTimeSecuredOperationService = oneTimeSecuredOperationService;
            _passwordHasher = passwordHasher;
        }

        public async Task ChangeAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }
            if (user.Value.Provider != ProviderType.Healthy)
            {
                throw new ServiceException(ErrorCodes.InvalidAccountType,
                    $"Password can not be changed for the account type: ;{user.Value.Provider}'.");
            }
            if (!user.Value.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new ServiceException(ErrorCodes.InvalidCurrentPassword,
                    "Current password is invalid.");
            }

            user.Value.SetPassword(newPassword, _passwordHasher);
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task ResetAsync(Guid operationId, string email)
        {
            var user = await _userRepository.GetByEmailAsync(email, ProviderType.Healthy.Name);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }
            await _oneTimeSecuredOperationService.CreateAsync(operationId, OneTimeSecuredOperationType.ResetPassword.Name,
                email, DateTime.UtcNow.AddDays(1));
        }

        public async Task SetNewAsync(string email, string token, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email, ProviderType.Healthy.Name);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email: '{email}' has not been found.");
            }

            await _oneTimeSecuredOperationService.ConsumeAsync(OneTimeSecuredOperationType.ResetPassword.Name,
                email, token);
            user.Value.SetPassword(password, _passwordHasher);
            await _userRepository.UpdateAsync(user.Value);
        }
    }
}