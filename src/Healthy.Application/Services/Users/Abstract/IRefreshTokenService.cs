using Healthy.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IRefreshTokenService
    {
        Task AddAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);
    }
}