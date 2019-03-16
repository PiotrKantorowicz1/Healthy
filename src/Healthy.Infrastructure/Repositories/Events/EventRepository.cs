using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Healthy.Core.Domain.Events.DomainClasses;
using Healthy.Core.Domain.Events.Repositories;
using Healthy.Core.Types;
using Healthy.Infrastructure.Repositories.Events.Queries;
using MongoDB.Driver;

namespace Healthy.Infrastructure.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoDatabase _database;

        public EventRepository(IMongoDatabase database)
        {
            _database = database;
        }
        
        public async Task<Maybe<IEnumerable<EventInfo>>> BrowseAsync(Expression<Func<EventInfo, bool>> predicate)
        {
            return await _database.Events()
                .Query(predicate)               
                .ToListAsync();
        }
       
        public async Task AddAsync(EventInfo @event)
            => await _database.Events().InsertOneAsync(@event);

        public async Task DeleteAsync(Guid id)
            => await _database.Events().DeleteOneAsync(x => x.Id == id);
    }
}