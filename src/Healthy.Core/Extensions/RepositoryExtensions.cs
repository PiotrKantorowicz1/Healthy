using System.Threading.Tasks;
using Healthy.Common.Exceptions;
using Healthy.Common.Extensions;
using Healthy.Core.Entities.Users;
using Healthy.Core.Repositories;

namespace Healthy.Core.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository,string userId)
            => await repository
                .GetByUserIdAsync(userId)
                .UnwrapAsync(noValueException: new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' does not exist!"));
    }
}