using System;
using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Users
{
    public class GetUserSessionBase : PagedQueryBase
    {
        public Guid Id { get; set; }
    }
}