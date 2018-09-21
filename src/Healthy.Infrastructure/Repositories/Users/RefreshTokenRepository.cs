using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Infrastructure.Repositories.Users.Queries;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Healthy.Infrastructure.Repositories.Users
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoDatabase _database;

        public RefreshTokenRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _database.RefreshTokens().GetAsync(token);

        public async Task AddAsync(RefreshToken token)
            => await _database.RefreshTokens().InsertOneAsync(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _database.RefreshTokens().ReplaceOneAsync(x => x.Id == token.Id, token);
    }
}