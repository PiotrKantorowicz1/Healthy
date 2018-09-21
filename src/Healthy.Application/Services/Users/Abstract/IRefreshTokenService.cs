using Healthy.Infrastructure.Security;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IRefreshTokenService : IService
    {
        Task AddAsync(string userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, string userId);
    }
}