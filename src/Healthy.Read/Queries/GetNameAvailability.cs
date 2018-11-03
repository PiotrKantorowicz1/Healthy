using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries
{
    public class GetNameAvailability : PagedQueryBase, IQuery<AvailableResourceDto>
    {
        public string Name { get; set; }
    }
}