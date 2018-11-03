using System;
using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Handlers.Users
{
    public sealed class ResetPasswordHandler : ICommandHandler<ResetPassword>
    {
        private readonly IHandler _handler;
        private readonly IPasswordService _passwordService;
        private readonly IOneTimeSecuredOperationService _oneTimeSecuredOperationService;


        public ResetPasswordHandler(IHandler handler, 
            IPasswordService passwordService, 
            IOneTimeSecuredOperationService oneTimeSecuredOperationService)
        {
            _handler = handler;
            _passwordService = passwordService;
            _oneTimeSecuredOperationService = oneTimeSecuredOperationService;
        }

        public async Task HandleAsync(ResetPassword command)
        {
            var operationId = Guid.NewGuid();
            await _handler
                .Run(async () => await _passwordService.ResetAsync(operationId, command.Email))
                //TODO send reset password message
                .OnError((ex, logger) => logger.Error(ex, "Error occured while resetting password"))
                .ExecuteAsync();
        }
    }
}