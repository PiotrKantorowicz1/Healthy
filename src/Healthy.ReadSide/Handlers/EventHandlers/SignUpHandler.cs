using System.Threading.Tasks;
using Healthy.Contracts.Events.Users;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Infrastructure.Handlers;
using Healthy.ReadSide.Models;
using Healthy.ReadSide.Services;

namespace Healthy.ReadSide.Handlers.EventHandlers
{
    public class SignUpHandler : IEventHandler<SignedUp>
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
                    var userReadModel = new UserRM
                    {
                        UserId = user.Value.UserId,
                        Email = user.Value.Email,
                        Name = user.Value.Name,
                        Provider = user.Value.Provider,
                        Role = user.Value.Role,
                        State = user.Value.State,
                        ExternalUserId = user.Value.ExternalUserId,
                        AvatarUrl = user.Value.Avatar.Url,
                        CreatedAt = user.Value.CreatedAt,
                    };

                    await _userCache.AddAsync(userReadModel);
                })
                .OnError((ex, logger) => logger.Error(ex, 
                    $"Error occured while handling {@event.GetType().Name} event"))
                .ExecuteAsync();
    }
}