using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries
{
    public class GetUserInfo : PagedQueryBase, IQuery<UserInfoDto>
    {
        public string UserId { get; set; }
    }
}