using Healthy.Application.Services.Users.Abstract;
using Healthy.Core;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClaimsProvider _claimsProvider;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtHandler jwtHandler,
            IPasswordHasher<User> passwordHasher,
            IClaimsProvider claimsProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _claimsProvider = claimsProvider;
        }

        public async Task AddAsync(string userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User: '{userId}' was not found.");
            }
            await _refreshTokenRepository.AddAsync(new RefreshToken(user.Value, _passwordHasher));
        }

        public async Task<JsonWebToken> CreateAccessTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null)
            {
                throw new ServiceException(ErrorCodes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new ServiceException(ErrorCodes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{refreshToken.Id}' was revoked.");
            }
            var user = await _userRepository.GetByUserIdAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User: '{refreshToken.UserId}' was not found.");
            }
            var claims = await _claimsProvider.GetAsync(user.Value.UserId);
            var jwt = _jwtHandler.CreateToken(user.Value.UserId, user.Value.Role, user.Value.State, claims);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeAsync(string token, string userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null || refreshToken.UserId != userId)
            {
                throw new ServiceException(ErrorCodes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }
            refreshToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(refreshToken);
        }
    }
}