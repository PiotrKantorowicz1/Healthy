using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Types;
using Healthy.Infrastructure.Repositories.Users.Queries;
using MongoDB.Driver;

namespace Healthy.Infrastructure.Repositories.Users
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly IMongoDatabase _database;

        public UserSessionRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<UserSession>> GetByIdAsync(Guid id)
            => await _database.UserSessions().GetByIdAsync(id);

        public async Task AddAsync(UserSession session)
            => await _database.UserSessions().InsertOneAsync(session);

        public async Task UpdateAsync(UserSession session)
            => await _database.UserSessions().ReplaceOneAsync(x => x.Id == session.Id, session);

        public async Task DeleteAsync(Guid id)
            => await _database.UserSessions().DeleteOneAsync(x => x.Id == id);
    }
}
