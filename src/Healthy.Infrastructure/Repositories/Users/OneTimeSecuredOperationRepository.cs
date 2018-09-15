using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Types;
using Healthy.Infrastructure.Repositories.Users.Queries;
using MongoDB.Driver;

namespace Healthy.Infrastructure.Repositories.Users
{
    public class OneTimeSecuredOperationRepository : IOneTimeSecuredOperationRepository
    {
        private readonly IMongoDatabase _database;

        public OneTimeSecuredOperationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<OneTimeSecuredOperation>> GetAsync(Guid id)
            => await _database.OneTimeSecuredOperations().GetAsync(id);

        public async Task<Maybe<OneTimeSecuredOperation>> GetAsync(string type, string user, string token)
            => await _database.OneTimeSecuredOperations().GetAsync(type, user, token);

        public async Task AddAsync(OneTimeSecuredOperation operation)
            => await _database.OneTimeSecuredOperations().InsertOneAsync(operation);

        public async Task UpdateAsync(OneTimeSecuredOperation operation)
            => await _database.OneTimeSecuredOperations().ReplaceOneAsync(x => x.Id == operation.Id, operation);
    }
}