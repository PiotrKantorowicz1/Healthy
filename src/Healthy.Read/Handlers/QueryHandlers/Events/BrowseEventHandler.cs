using System.Linq;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Pagination;
using Healthy.EventStore.EventsStore;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Dtos.Events;
using Healthy.Read.Dtos.Users;
using Healthy.Read.Mappers.Events;
using Healthy.Read.Queries;
using Healthy.Read.Queries.Events;

namespace Healthy.Read.Handlers.QueryHandlers.Events
{
    public sealed class BrowseEventHandler : IQueryHandler<BrowseEvents, PagedResult<EventInfoDto>>
    {
        private readonly IEventStore _eventStore;
        private readonly IEventMapper _eventMapper;

        public BrowseEventHandler(IEventStore eventStore, IEventMapper eventMapper)
        {
            _eventStore = eventStore;
            _eventMapper = eventMapper;
        }

        public async Task< <EventInfoDto>> HandleAsync(BrowseEvents query)
        {
            var pagedResult = await _eventStore.Load(query.Id, query.Version);
            var events = pagedResult.Select(x => _eventMapper.MapToEventInfoDto(x));
            return events;
        }
    }
}