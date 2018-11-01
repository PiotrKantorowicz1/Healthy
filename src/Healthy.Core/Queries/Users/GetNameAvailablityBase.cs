using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetNameAvailabilityBase : PagedQueryBase
    {
        public string Name { get; set; }
    }
}