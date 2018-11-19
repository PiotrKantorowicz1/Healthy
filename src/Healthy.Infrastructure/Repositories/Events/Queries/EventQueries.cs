using System;
using System.Linq.Expressions;
using Healthy.Core.Domain.Events.DomainClasses;
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
            Expression<Func<EventInfo, bool>> predicate)
        {
            var values = @events.AsQueryable().Where(predicate);
            return values;
        }
    }
}