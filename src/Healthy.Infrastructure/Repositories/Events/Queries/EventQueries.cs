using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Core.Queries.Events;
using Healthy.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Healthy.Infrastructure.Repositories.Events.Queries
{
    public static class EventQueries
    {
        public static IMongoCollection<EventInfo> Events(this IMongoDatabase database)
            => database.GetCollection<EventInfo>();
        
        public static IMongoQueryable<EventInfo> Query(this IMongoCollection<EventInfo> @events,
            BrowseEvents query)
        {
            var values = @events.AsQueryable();

            return values.OrderBy(x => x.Name);
        }
    }
}