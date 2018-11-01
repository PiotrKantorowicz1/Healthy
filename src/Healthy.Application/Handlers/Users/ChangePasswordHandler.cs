using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Application.Handlers.Users
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