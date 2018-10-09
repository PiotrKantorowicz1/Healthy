using System;
using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Application.Dispatchers;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Core.Types;
using Healthy.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IJwtHandler _jwtHandler;
        
        public AuthenticationController(ICommandDispatcher commandDispatcher, IUserService userService, 
            IAuthenticationService authenticationService, IJwtHandler jwtHandler) : base(commandDispatcher)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
        
        private async Task<Maybe<JwtSession>> HandleSessionAsync(Guid sessionId) 
        {
            var session = await _authenticationService.GetSessionAsync(sessionId);
            if (session.HasNoValue)
            {
                return null;
            }
            var user = await _userService.GetAsync(session.Value.UserId);
            var token = _jwtHandler.CreateToken(user.Value.UserId, 
                user.Value.Role, state: user.Value.State);

            return new JwtSession
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                Expires = token.Expires,
                SessionId = session.Value.Id,
                Role = token.Role,
                Key = session.Value.Key
            };
        }        
    }
}
