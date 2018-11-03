using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Handlers.Users
{
    public sealed class ChangeUsernameHandler : ICommandHandler<ChangeUsername>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public ChangeUsernameHandler(IHandler handler,
            IUserService userService)
        {
            _handler = handler;
            _userService = userService;
        }

        public async Task HandleAsync(ChangeUsername command)
            => await _handler
                .Run(async () => await _userService.ChangeNameAsync(command.UserId, command.Name))
                .OnError((ex, logger) => logger.Error(ex, "Error occured while changing username"))
                .ExecuteAsync();
    }
}