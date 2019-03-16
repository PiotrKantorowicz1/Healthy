using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Enumerations;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Core.Types;

namespace Healthy.Services.Services.Users.Abstract
{
    public interface IUserService : IService
    {
        Task<bool> IsNameAvailableAsync(string name);
        Task<Maybe<User>> GetAsync(Guid userId);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId);
        Task<Maybe<User>> GetByEmailAsync(string email, string provider);
        Task<Maybe<States>> GetStateAsync(Guid userId);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query);

        Task SignUpAsync(Guid id, Guid userId, string email, string role,
            string provider, string password = null,
            string externalUserId = null,
            bool activate = true, string name = null);

        Task ChangeNameAsync(Guid userId, string name);
        Task ActivateAsync(string email, string token);
        Task LockAsync(Guid userId);
        Task UnlockAsync(Guid userId);
        Task DeleteAsync(Guid userId, bool soft);
    }
}