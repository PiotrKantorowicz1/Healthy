using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Enumerations;
using Healthy.Core.Extensions;
using Healthy.Core.Queries.Users;
using Healthy.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Healthy.Infrastructure.Repositories.Users.Queries
{
    public static class UserQueries
    {
        public static IMongoCollection<User> Users(this IMongoDatabase database)
            => database.GetCollection<User>();

        public static async Task<bool> ExistsAsync(this IMongoCollection<User> users, string name)
            => await users.AsQueryable().AnyAsync(x => x.Name == name);

        public static async Task<User> GetOwnerAsync(this IMongoCollection<User> users)
            => await users.AsQueryable().FirstOrDefaultAsync(x => x.Role == Roles.Owner);

        public static async Task<User> GetByUserIdAsync(this IMongoCollection<User> users, Guid userId)
        {
            if (userId == Guid.Empty)
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public static async Task<User> GetByExternalUserIdAsync(this IMongoCollection<User> users, string externalUserId)
        {
            if (externalUserId.Empty())
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.ExternalUserId == externalUserId);
        }

        public static async Task<User> GetByEmailAsync(this IMongoCollection<User> users, string email, string provider)
        {
            if (email.Empty())
                return null;
            if (provider.Empty())
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email && x.Provider.Name == provider);
        }

        public static async Task<User> GetByNameAsync(this IMongoCollection<User> users, string name)
        {
            if (name.Empty())
                return null;

            return await users.AsQueryable().FirstOrDefaultAsync(x => x.Name == name);
        }

        public static async Task<States> GetStateAsync(this IMongoCollection<User> users, Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return await users.AsQueryable().Where(x => x.UserId == id)
                .Select(x => x.State)
                .FirstOrDefaultAsync();
        }

        public static IMongoQueryable<User> Query(this IMongoCollection<User> users,
            BrowseUsersBase query)
        {
            var values = users.AsQueryable();

            return values.OrderBy(x => x.Name);
        }
    }
}