using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Services.Handlers.Users
{
    public sealed class UnlockAccountHandler : ICommandHandler<UnlockAccount>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public UnlockAccountHandler(IHandler handler,
            IUserService userService)
        {
            _handler = handler;
            _userService = userService;
        }

        public async Task HandleAsync(UnlockAccount command)
            => await _handler
                .Run(async () => await _userService.UnlockAsync(command.UnlockUserId))
                .OnError((ex, logger) =>
                    logger.Error($"Error occured while unlocking an account for user: '{command.UserId}'."))
                .ExecuteAsync();
    }
}