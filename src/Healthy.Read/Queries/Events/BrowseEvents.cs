using System;
using System.Collections.Generic;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Events;
using Healthy.Read.Dtos.Events;

namespace Healthy.Read.Queries.Events
{
    public class BrowseEvents : BrowseEventsBase, IQuery<IEnumerable<EventInfoDto>>
    {
        public Guid Id { get; set; }
    }
}