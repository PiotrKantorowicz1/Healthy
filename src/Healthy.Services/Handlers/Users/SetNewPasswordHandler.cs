using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Services.Handlers.Users
{
    public sealed class SetNewPasswordHandler : ICommandHandler<SetNewPassword>
    {
        private readonly IHandler _handler;
        private readonly IPasswordService _passwordService;

        public SetNewPasswordHandler(IHandler handler,
            IPasswordService passwordService)
        {
            _handler = handler;
            _passwordService = passwordService;
        }

        public async Task HandleAsync(SetNewPassword command)
            => await _handler
                .Run(async () => await _passwordService.SetNewAsync(command.Email, 
                    command.Token, command.Password))
                .OnError((ex, logger) => logger.Error(ex, "Error when trying to set new password."))
                .ExecuteAsync();
    }
}