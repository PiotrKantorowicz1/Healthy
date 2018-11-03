using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUserBase : PagedQueryBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}