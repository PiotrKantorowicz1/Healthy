using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries
{
    public class GetUserState : PagedQueryBase, IQuery<UserStateDto>
    {
        public string UserId { get; set; }
    }
}