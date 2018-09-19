using System;
using System.Net;
using System.Threading.Tasks;
using Healthy.Api.Modules.Base;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Core.Contracts.Commands.Users;
using Healthy.Core.Types;
using Healthy.Infrastructure.Handlers;
using Healthy.Infrastructure.Security;

namespace Healthy.Api.Modules
{
    public class AuthenticationModule : ModuleBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public AuthenticationModule(
            IAuthenticationService authenticationService,
            IUserService userService,
            IJwtTokenHandler jwtTokenHandler,
            ICommandHandler<SignIn> signInHandler,
            ICommandHandler<RefreshUserSession> refreshSessionHandler)
            : base(requireAuthentication: false)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _jwtTokenHandler = jwtTokenHandler;

            Post("sign-in", async args =>
            {
                var command = BindRequest<SignIn>();
                await signInHandler.HandleAsync(command);
                command.SessionId = Guid.NewGuid();
                command.IpAddress = Request.UserHostAddress;
                command.UserAgent = Request.Headers.UserAgent;
                var session = await HandleSessionAsync(command.SessionId);
                if (session.HasNoValue)
                {
                    return HttpStatusCode.Unauthorized;
                }

                return session.Value;
            });

            Post("sessions", async args =>
            {
                var command = BindRequest<RefreshUserSession>();
                await refreshSessionHandler.HandleAsync(command);
                var session = await HandleSessionAsync(command.NewSessionId);
                if (session.HasNoValue)
                {
                    return HttpStatusCode.Forbidden;
                }

                return session.Value;
            });
        }

        private async Task<Maybe<JwtSession>> HandleSessionAsync(Guid sessionId)
        {
            var session = await _authenticationService.GetSessionAsync(sessionId);
            if (session.HasNoValue)
            {
                return null;
            }
            var user = await _userService.GetAsync(session.Value.UserId);
            var token = _jwtTokenHandler.Create(user.Value.UserId,
                user.Value.Role, state: user.Value.State);

            return new JwtSession
            {
                Token = token.Value.Token,
                Expires = token.Value.Expires,
                SessionId = session.Value.Id,
                Key = session.Value.Key
            };
        }
    }
}