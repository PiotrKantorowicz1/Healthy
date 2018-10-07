using Healthy.Core.Extensions;
using Healthy.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Infrastructure.Repositories.Users.Queries
{
    public static class RefreshTokenQueries
    {
        public static IMongoCollection<RefreshToken> RefreshTokens(this IMongoDatabase database)
            => database.GetCollection<RefreshToken>();

        public static async Task<RefreshToken> GetAsync(this IMongoCollection<RefreshToken> tokens, string token)
        {
            if (token.Empty())
                return null;

            return await tokens.AsQueryable().FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}
