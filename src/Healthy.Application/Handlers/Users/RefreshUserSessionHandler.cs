using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Application.Handlers.Users
{
    public class RefreshUserSessionHandler : ICommandHandler<RefreshUserSession>
    {
        private readonly IHandler _handler;
        private readonly IAuthenticationService _authenticationService;

        public RefreshUserSessionHandler(IHandler handler,
            IAuthenticationService authenticationService)
        {
            _handler = handler;
            _authenticationService = authenticationService;
        }

        public async Task HandleAsync(RefreshUserSession command)
            => await _handler
                .Run(async () => await _authenticationService.RefreshSessionAsync(command.SessionId, 
                    command.NewSessionId, command.Key))
                .OnError((ex, logger) => logger.Error(ex, "Error when refreshing user session."))
                .ExecuteAsync();
    }
}