using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Domain.Users.Repositories;
using System;

namespace Healthy.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid userId)
            => await repository
                .GetByUserIdAsync(userId)
                .UnwrapAsync(noValueException: new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' does not exist!"));
    }
}