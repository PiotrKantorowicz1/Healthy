using System;
using Healthy.Core.Pagination;

namespace Healthy.Core.Queries.Events
{
    public class BrowseEventsBase : PagedQueryBase
    {
        public Guid AggregateId { get; set; }
        public int? Version { get; set; }
    }
}