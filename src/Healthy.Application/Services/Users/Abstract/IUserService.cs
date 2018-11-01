using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Core.Types;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IUserService : IService
    {
        Task<bool> IsNameAvailableAsync(string name);
        Task<Maybe<User>> GetAsync(string userId);
        Task<Maybe<User>> GetByNameAsync(string name);
        Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId);
        Task<Maybe<User>> GetByEmailAsync(string email, string provider);
        Task<Maybe<string>> GetStateAsync(string userId);
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query);

        Task SignUpAsync(string userId, string email, string role,
            string provider, string password = null,
            string externalUserId = null,
            bool activate = true, string name = null);

        Task ChangeNameAsync(string userId, string name);
        Task ActivateAsync(string email, string token);
        Task LockAsync(string userId);
        Task UnlockAsync(string userId);
        Task DeleteAsync(string userId, bool soft);
    }
}