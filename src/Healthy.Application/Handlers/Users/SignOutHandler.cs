using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Application.Handlers.Users
{
    public class SignOutHandler : ICommandHandler<SignOut>
    {
        private readonly IHandler _handler;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccessTokenService _accessTokenService;

        public SignOutHandler(IHandler handler,
            IAuthenticationService authenticationService,
            IAccessTokenService accessTokenService)
        {
            _handler = handler;
            _authenticationService = authenticationService;
            _accessTokenService = accessTokenService;
        }

        public async Task HandleAsync(SignOut command)
        {
            await _handler
                .Run(async () => 
                {
                    await _authenticationService.SignOutAsync(command.SessionId, command.UserId);
                    await _accessTokenService.DeactivateCurrentAsync(command.UserId);
                })
                .OnError((ex, logger) =>
                {
                    logger.Error("Error occured while signing out");
                })
                .ExecuteAsync();
        }
    }
}
