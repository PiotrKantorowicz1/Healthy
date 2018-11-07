using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Events;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Events.Repositories
{
    public interface IEventRepository : IRepository
    {
        Task<Maybe<PagedResult<EventInfo>>> BrowseAsync(BrowseEvents events);
        Task AddAsync(EventInfo @event);
        Task DeleteAsync(Guid @id);
    }
}