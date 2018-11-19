using System;
using System.Linq;
using System.Threading.Tasks;
using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Read.Dtos.Events;

namespace Healthy.Read.Mappers.Events
{
    public class EventMapper : IEventMapper
    {
        public EventInfoDto MapToEventInfoDto(EventInfo @event)
            => new EventInfoDto
            {
                AggregateId = @event.AggregateId,
                Data = @event.Data,
                Id = @event.Id,
                Name = @event.Name,
                Timestamp = @event.Timestamp,
                Version = @event.Version
            };
    }
}