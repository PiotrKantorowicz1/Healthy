using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries.Users
{
    public class GetUserInfo : PagedQueryBase, IQuery<UserInfoDto>
    {
        public string UserId { get; set; }
    }
}