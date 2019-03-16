using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries.Users
{
    public class GetUserInfoByName : PagedQueryBase, IQuery<UserInfoDto>
    {
        public string Name { get; set; }
    }
}