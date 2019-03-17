using System.Threading.Tasks;
using Healthy.Core.Pagination;
using Healthy.Core.Types;
using Healthy.Core.Queries.Users;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Users.DomainClasses;
using System;
using Healthy.Core.Domain.Users.Enumerations;

namespace Healthy.Core.Domain.Users.Repositories
{
    public interface IUserRepository : IRepository
    {
        Task<bool> ExistsAsync(string name); 
        Task<Maybe<User>> GetOwnerAsync();
        Task<Maybe<User>> GetByUserIdAsync(Guid userId);
        Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId);
        Task<Maybe<User>> GetByEmailAsync(string email, string provider);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task<Maybe<States>> GetStateAsync(Guid userId);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
    }
}
