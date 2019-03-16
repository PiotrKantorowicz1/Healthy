using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries.Users
{
    public class GetUserState : PagedQueryBase, IQuery<UserStateDto>
    {
        public string UserId { get; set; }
    }
}