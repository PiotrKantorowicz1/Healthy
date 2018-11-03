using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Handlers.Users
{
    public sealed class ActivateAccountHandler : ICommandHandler<ActivateAccount>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public ActivateAccountHandler(IHandler handler,
            IUserService userService)
        {
            _handler = handler;
            _userService = userService;
        }

        public async Task HandleAsync(ActivateAccount command)
            => await _handler
                .Run(async () => await _userService.ActivateAsync(command.Email, command.Token))
                .OnError((ex, logger) => logger.Error(ex, "Error when activating account."))
                .ExecuteAsync();
    }
}