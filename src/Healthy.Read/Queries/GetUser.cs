using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries
{
    public class GetUser : PagedQueryBase, IQuery<UserDto>
    {
        public string UserId { get; set; }
    }
}