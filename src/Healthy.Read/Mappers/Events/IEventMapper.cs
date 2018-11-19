using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Read.Dtos.Events;

namespace Healthy.Read.Mappers.Events
{
    public interface IEventMapper : IMapper
    {
        EventInfoDto MapToEventInfoDto(EventInfo @event);
    }
}