using System.Threading.Tasks;
using Healthy.Contracts.Events.Users;
using Healthy.Core;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Infrastructure.Handlers;
using Healthy.Storage.Caching;

namespace Healthy.Storage.Handlers.EventHandlers
{
    public sealed class SignUpHandler : IEventHandler<SignedUp>
    {
        private readonly IHandler _handler;
        private readonly IUserRepository _userRepository;
        private readonly IUserCache _userCache;


        public SignUpHandler(IHandler handler,
            IUserRepository userRepository,
            IUserCache userCache)
        {
            _handler = handler;
            _userRepository = userRepository;
            _userCache = userCache;
        }

        public async Task HandleAsync(SignedUp @event)
            => await _handler
                .Run(async () =>
                {
                    var user = await _userRepository.GetByUserIdAsync(@event.UserId);
                    if (user.HasNoValue)
                    {
                        throw new ServiceException(ErrorCodes.UserNotFound,
                            $"User from event {@event.GetType().Name} not found");
                    }
                    await _userCache.AddAsync(user.Value);
                })
                .OnError((ex, logger) => logger.Error(ex, 
                    $"Error occured while handling {@event.GetType().Name} event"))
                .ExecuteAsync();
    }
}