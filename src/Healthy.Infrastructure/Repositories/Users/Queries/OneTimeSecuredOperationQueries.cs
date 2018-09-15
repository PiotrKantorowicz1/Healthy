using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Extensions;
using Healthy.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Healthy.Infrastructure.Repositories.Users.Queries
{
    public static class OneTimeSecuredOperationQueries
    {
        public static IMongoCollection<OneTimeSecuredOperation> OneTimeSecuredOperations(this IMongoDatabase database)
            => database.GetCollection<OneTimeSecuredOperation>();

        public static async Task<OneTimeSecuredOperation> GetAsync(this IMongoCollection<OneTimeSecuredOperation> operations,
            Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await operations.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<OneTimeSecuredOperation> GetAsync(this IMongoCollection<OneTimeSecuredOperation> operations,
            string type, string user, string token)
        {
            if (type.Empty())
                return null;
            if (user.Empty())
                return null;
            if (token.Empty())
                return null;

            return await operations.AsQueryable().FirstOrDefaultAsync(x => x.Type == type &&
                                                                           x.User == user && x.Token == token);
        }
    }
}