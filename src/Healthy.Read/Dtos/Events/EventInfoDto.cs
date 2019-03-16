using System;
using Healthy.Contracts.Events;

namespace Healthy.Read.Dtos.Events
{
    public class EventInfoDto
    {
        public Guid Id { get; set; }
        public Guid AggregateId { get; set; }
        public long Timestamp { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public IEvent Data { get; set; }
    }
}