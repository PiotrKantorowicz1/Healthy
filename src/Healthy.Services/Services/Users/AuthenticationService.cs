using System;
using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Domain.Users.Services;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;
using Healthy.Infrastructure.Security;
using Healthy.Services.Services.Users.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Healthy.Services.Services.Users
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IFacebookService _facebookService;
        private readonly IUserService _userService;
        private readonly IClaimsProvider _claimsProvider;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEncrypter _encrypter;

        public AuthenticationService(IUserRepository userRepository,
            IUserSessionRepository userSessionRepository,
            IFacebookService facebookService,
            IPasswordHasher<User> passwordHasher,
            IEncrypter encrypter,
            IUserService userService,
            IClaimsProvider claimsProvider,
            IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _userSessionRepository = userSessionRepository;
            _facebookService = facebookService;
            _userService = userService;
            _claimsProvider = claimsProvider;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _encrypter = encrypter;
        }

        public async Task<Maybe<UserSession>> GetSessionAsync(Guid id)
            => await _userSessionRepository.GetByIdAsync(id);

        public async Task<Maybe<JwtSession>> HandleSessionAsync(Guid sessionId)
        {
            var session = await GetSessionAsync(sessionId);
            if (session.HasNoValue)
            {
                return null;
            }

            var user = await _userService.GetAsync(session.Value.UserId);
            var claims = await _claimsProvider.GetAsync(user.Value.UserId);
            var token = _jwtHandler.CreateToken(user.Value.UserId,
                user.Value.Role, state: user.Value.State, claims: claims);

            return new JwtSession
            {
                AccessToken = token.AccessToken,
                Expires = token.Expires,
                SessionId = session.Value.Id,
                Role = token.Role,
                Key = session.Value.Key
            };
        }

        public async Task SignInAsync(Guid sessionId, string email, string password,
            string ipAddress = null, string userAgent = null)
        {
            var user = await _userRepository.GetByEmailAsync(email, Providers.Healthy);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with email '{email}' has not been found.");
            }
            if (user.Value.State != States.Active && user.Value.State != States.Unconfirmed)
            {
                throw new ServiceException(ErrorCodes.InactiveUser,
                    $"User '{user.Value.Id}' is not active.");
            }
            if (!user.Value.ValidatePassword(password, _passwordHasher))
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials,
                    "Invalid credentials.");
            }
            await CreateSessionAsync(sessionId, user.Value);
        }

        public async Task SignInViaFacebookAsync(Guid sessionId, string accessToken,
            string ipAddress = null, string userAgent = null)
        {
            var facebookUser = await _facebookService.GetUserAsync(accessToken);
            if (facebookUser.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"Facebook user has not been found for given access token.");
            }
            var user = await _userRepository.GetByExternalUserIdAsync(facebookUser.Value.Id);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with Facebook external id: " +
                    $"'{facebookUser.Value.Id}' has not been found.");
            }
            if (user.Value.State != States.Active && user.Value.State != States.Incomplete)
            {
                throw new ServiceException(ErrorCodes.InactiveUser,
                    $"User '{user.Value.Id}' is neither active nor incomplete.");
            }
            await CreateSessionAsync(sessionId, user.Value);
        }

        public async Task SignOutAsync(Guid sessionId, string userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id '{userId}' has not been found.");
            }

            var session = await _userSessionRepository.GetByIdAsync(sessionId);
            if (session.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.SessionNotFound,
                    $"Session with id '{sessionId}' has not been found.");
            }
            session.Value.Destroy();
            await _userSessionRepository.UpdateAsync(session.Value);
        }

        public async Task CreateSessionAsync(Guid sessionId, string userId,
            string ipAddress = null,
            string userAgent = null)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id '{userId}' has not been found.");
            }
            await CreateSessionAsync(sessionId, user.Value);
        }

        private async Task CreateSessionAsync(Guid sessionId, User user,
            string ipAddress = null, string userAgent = null)
        {
            var session = new UserSession(sessionId, user.UserId,
                _encrypter.GetRandomSecureKey(), ipAddress, userAgent);
            await _userSessionRepository.AddAsync(session);
        }

        public async Task RefreshSessionAsync(Guid sessionId, Guid newSessionId,
            string sessionKey, string ipAddress = null, string userAgent = null)
        {
            var parentSession = await _userSessionRepository.GetByIdAsync(sessionId);
            if (parentSession.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.SessionNotFound,
                    $"Session with id '{sessionId}' has not been found.");
            }
            if (parentSession.Value.Key != sessionKey)
            {
                throw new ServiceException(ErrorCodes.InvalidSessionKey,
                    $"Invalid session key: '{sessionKey}'");
            }
            var newSession = parentSession.Value.Refresh(newSessionId,
                _encrypter.GetRandomSecureKey(), sessionId, ipAddress, userAgent);
            await _userSessionRepository.AddAsync(newSession);
            await _userSessionRepository.DeleteAsync(sessionId);
        }
    }
}
