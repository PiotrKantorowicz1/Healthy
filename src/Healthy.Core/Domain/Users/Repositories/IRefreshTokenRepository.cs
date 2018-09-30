using Healthy.Core.Types;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Core.Domain.Users.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<Maybe<RefreshToken>> GetAsync(string token);
        Task AddAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}