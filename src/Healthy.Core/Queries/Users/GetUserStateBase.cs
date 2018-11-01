using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUserStateBase : PagedQueryBase
    {
        public string Id { get; set; }
    }
}

