using System.Threading.Tasks;
using Healthy.Core.Pagination;
using Healthy.Core.Types;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Queries.Users;

namespace Healthy.Core.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsAsync(string name); 
        Task<Maybe<User>> GetOwnerAsync();
        Task<Maybe<User>> GetByUserIdAsync(string userId);
        Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId);
        Task<Maybe<User>> GetByEmailAsync(string email, string provider);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task<Maybe<string>> GetStateAsync(string userId);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsers query);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string userId);
    }
}
