using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Core.Domain.Events.Repositories;

namespace Healthy.EventStore.EventsStore
{
    public class EventStore : IEventStore
    {
        private readonly List<EventInfo> _events = new List<EventInfo>();
        private readonly IEventRepository _eventRepository;

        public EventStore(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public T Load<T>(Guid aggregateId, int? version = null) where T : AggregateRoot, new()
        {
            version = version ?? int.MaxValue;
            var events = _events
                .Where(e => e.AggregateId == aggregateId && e.Version <= version)
                .OrderBy(x => x.Version)
                .Select(e => e.Data).ToList();

            if (!events.Any())
            {
                return null;
            }

            var aggregate = new T();
            aggregate.Replay(events);

            return aggregate;
        }

        public void Store<T>(T aggregate) where T : AggregateRoot
        {
            _events.AddRange(aggregate.Events
                .Select(e => new EventInfo
                {
                    Id = e.Id,
                    AggregateId = aggregate.Id,
                    Timestamp = DateTime.UtcNow.Ticks,
                    Version = aggregate.Version,
                    Name = e.GetType().Name,
                    Data = e
                }));

            foreach (var @event in _events)
            {
                _eventRepository.AddAsync(@event);
            }
        }
    }
}