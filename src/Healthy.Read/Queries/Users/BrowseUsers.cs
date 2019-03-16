using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries.Users
{
    public class BrowseUsers : BrowseUsersBase, IQuery<PagedResult<UserDto>>
    {    
    }
}