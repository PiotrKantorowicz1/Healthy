using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Services.Handlers.Users
{
    public sealed class LockAccountHandler : ICommandHandler<LockAccount>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public LockAccountHandler(IHandler handler,
            IUserService userService)
        {
            _handler = handler;
            _userService = userService;
        }

        public async Task HandleAsync(LockAccount command)
            => await _handler
                .Run(async () => await _userService.LockAsync(command.LockUserId))
                .OnError((ex, logger) =>
                    logger.Error($"Error occured while locking an account for user: '{command.UserId}'."))
                .ExecuteAsync();
    }
}