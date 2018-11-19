using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Events.DomainClasses;

namespace Healthy.EventStore.EventsStore
{
    public interface IEventStore
    {
        T Load<T>(Guid aggregateId, int? version = null) where T : AggregateRoot, new();
        Task<IEnumerable<EventInfo>> Load(Guid aggregateId, int? version = null);
        void Store<T>(T aggregate) where T : AggregateRoot;
    }
}