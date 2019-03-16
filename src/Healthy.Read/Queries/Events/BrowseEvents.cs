using System;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Events;
using Healthy.Read.Dtos.Events;
using Healthy.Read.Dtos.Users;

namespace Healthy.Read.Queries.Events
{
    public class BrowseEvents : BrowseEventsBase, IQuery<PagedResult<EventInfoDto>>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}