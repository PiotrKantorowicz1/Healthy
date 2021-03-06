using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Enumerations;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Pagination;
using Healthy.Core.Queries.Users;
using Healthy.Core.Types;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Repositories.Users.Queries;
using MongoDB.Driver;

namespace Healthy.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<bool> ExistsAsync(string name)
            => await _database.Users().ExistsAsync(name);

        public async Task<Maybe<User>> GetOwnerAsync()
            => await _database.Users().GetOwnerAsync();

        public async Task<Maybe<User>> GetByUserIdAsync(Guid userId)
            => await _database.Users().GetByUserIdAsync(userId);

        public async Task<Maybe<User>> GetByExternalUserIdAsync(string externalUserId)
            => await _database.Users().GetByExternalUserIdAsync(externalUserId);

        public async Task<Maybe<User>> GetByEmailAsync(string email, string provider)
            => await _database.Users().GetByEmailAsync(email, provider);

        public async Task<Maybe<User>> GetByNameAsync(string name)
            => await _database.Users().GetByNameAsync(name);

        public async Task<Maybe<States>> GetStateAsync(Guid id)
            => await _database.Users().GetStateAsync(id);

        public async Task<Maybe<PagedResult<User>>> BrowseAsync(BrowseUsersBase query)
        {
            return await _database.Users()
                .Query(query)
                .PaginateAsync(query);
        }

        public async Task AddAsync(User user)
            => await _database.Users().InsertOneAsync(user);

        public async Task UpdateAsync(User user)
            => await _database.Users().ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task DeleteAsync(Guid userId)
            => await _database.Users().DeleteOneAsync(x => x.UserId == userId);
    }
}