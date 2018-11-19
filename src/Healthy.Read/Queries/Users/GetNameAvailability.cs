using Healthy.Core.Pagination;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries.Users
{
    public class GetNameAvailability : PagedQueryBase, IQuery<AvailableResourceDto>
    {
        public string Name { get; set; }
    }
}