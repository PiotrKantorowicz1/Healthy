using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries
{
    public class GetUserByName : PagedQueryBase, IQuery<UserDto>
    {
        public string Name { get; set; }
    }
}