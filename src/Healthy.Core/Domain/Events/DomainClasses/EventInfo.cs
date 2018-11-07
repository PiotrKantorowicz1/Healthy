using System;
using Healthy.Contracts.Events;

namespace Healthy.Core.Domain.Events.DomainClasses
{
    public class EventInfo
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public long Timestamp { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public IEvent Data { get; set; }
    }
}