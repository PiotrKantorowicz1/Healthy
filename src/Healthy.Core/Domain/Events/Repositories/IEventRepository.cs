using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Events.Repositories
{
    public interface IEventRepository : IRepository
    {
        Task<Maybe<IEnumerable<EventInfo>>> BrowseAsync(Expression<Func<EventInfo, bool>> predicate);
        Task AddAsync(EventInfo @event);
        Task DeleteAsync(Guid @id);
    }
}