using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.EventStore.EventsStore
{
    public interface IEventStore
    {
        T Load<T>(Guid aggregateId, int? version = null) where T : AggregateRoot, new();
        void Store<T>(T aggregate) where T : AggregateRoot;
    }
}