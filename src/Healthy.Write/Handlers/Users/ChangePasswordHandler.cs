using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Handlers.Users
{
    public sealed class ChangePasswordHandler : ICommandHandler<ChangePassword>
    {
        private readonly IHandler _handler;
        private readonly IPasswordService _passwordService;

        public ChangePasswordHandler(IHandler handler,
            IPasswordService passwordService)
        {
            _handler = handler;
            _passwordService = passwordService;
        }

        public async Task HandleAsync(ChangePassword command)
            => await _handler
                .Run(async () => await _passwordService.ChangeAsync(command.UserId,
                    command.CurrentPassword, command.NewPassword))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to change password."))
                .ExecuteAsync();
    }
}