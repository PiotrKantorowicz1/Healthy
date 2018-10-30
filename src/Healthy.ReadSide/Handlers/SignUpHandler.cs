using System.Threading.Tasks;
using Healthy.Contracts.Events.Users;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;
using Healthy.Infrastructure.Handlers;
using Healthy.ReadSide.Models;
using Healthy.ReadSide.Services;

namespace Healthy.ReadSide.Handlers
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
                    var user = _userRepository.GetByUserIdAsync(@event.UserId);
                    var userReadModel = new UserRM
                    {
                        UserId = user.Result.Value.UserId,
                        Email = user.Result.Value.Email,
                        Name = user.Result.Value.Name,
                        Provider = user.Result.Value.Provider,
                        Role = user.Result.Value.Role,
                        State = user.Result.Value.State,
                        ExternalUserId = user.Result.Value.ExternalUserId,
                        CreatedAt = user.Result.Value.CreatedAt,
                        UpdatedAt = user.Result.Value.UpdatedAt,
                        Avatar = new AvatarRM
                        {
                            IsEmpty = user.Result.Value.Avatar.IsEmpty,
                            Name = user.Result.Value.Avatar.Name,
                            Url = user.Result.Value.Avatar.Urlgit
                        }
                    };

                    await _userCache.AddAsync(userReadModel);
                })
                .OnError((ex, logger) => logger.Error(ex, 
                    $"Error occured while handling {@event.GetType().Name} event"))
                .ExecuteAsync();
    }
}