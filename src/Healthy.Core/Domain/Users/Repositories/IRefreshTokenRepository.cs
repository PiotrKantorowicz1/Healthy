using Healthy.Core.Domain.Users.Entities;
using System.Threading.Tasks;

namespace Healthy.Core.Domain.Users.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}