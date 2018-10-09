using System;
using System.Net;
using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Application.Dispatchers;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Types;
using Healthy.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ICommandDispatcher commandDispatcher,
            IAuthenticationService authenticationService) 
            : base(commandDispatcher)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("me")]
        public IActionResult Get() => Content($"Your id: '{UserId:N}'.");

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignIn command)
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
        public async Task<IActionResult> RefreshSession(RefreshUserSession command)
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
        public async Task<IActionResult> SignOut(SignOut command)
        {
            await DispatchAsync(command);

            return NoContent();
        }        
    }
}
