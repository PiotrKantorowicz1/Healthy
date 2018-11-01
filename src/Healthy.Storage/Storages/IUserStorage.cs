using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Core.Types;

namespace Healthy.Read.Storages
{
    public interface IUserStorage
    {
        Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query);
    }
}