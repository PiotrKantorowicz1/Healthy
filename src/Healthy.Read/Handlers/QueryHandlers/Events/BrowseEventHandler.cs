using System.Collections.Generic;
using System.Threading.Tasks;
using Healthy.EventStore.EventsStore;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Events;
using Healthy.Read.Mappers.Events;
using Healthy.Read.Queries.Events;

namespace Healthy.Read.Handlers.QueryHandlers.Events
{
    public sealed class BrowseEventHandler : IQueryHandler<BrowseEvents, IEnumerable<EventInfoDto>>
    {
        private readonly IEventStore _eventStore;
        private readonly IEventMapper _eventMapper;

        public BrowseEventHandler(IEventStore eventStore, IEventMapper eventMapper)
        {
            _eventStore = eventStore;
            _eventMapper = eventMapper;
        }

        public async Task<IEnumerable<EventInfoDto>> HandleAsync(BrowseEvents query)
        {
            //var result = await _eventStore.Load(query.Id, query.Version);
            //var events = result.Select(x => _eventMapper.MapToEventInfoDto(x));
            //return events;
            return null;
        }
    }
}