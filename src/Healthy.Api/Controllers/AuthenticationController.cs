using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Services.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IDispatcher dispatcher,
            IAuthenticationService authenticationService) 
            : base(dispatcher)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("me")]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(SignIn command)
        {
            await DispatchAsync(command.BindId(c => c.SessionId));
            var session = await _authenticationService.HandleSessionAsync(command.SessionId);
            if (session.HasNoValue)
            {
                return StatusCode(401);
            }

            return Ok(session.Value);
        }

        [HttpPost("sessions")]
        public async Task<IActionResult> Post(RefreshUserSession command)
        {
            await DispatchAsync(command.BindId(c => c.NewSessionId));
            var session = await _authenticationService.HandleSessionAsync(command.NewSessionId);
            if (session.HasNoValue)
            {
                return StatusCode(403);
            }

            return Ok(session.Value);
        }

        [HttpPost("sign-out")]
        public async Task<IActionResult> Post(SignOut command)
        {
            await DispatchAsync(command);

            return NoContent();
        }        
    }
}
