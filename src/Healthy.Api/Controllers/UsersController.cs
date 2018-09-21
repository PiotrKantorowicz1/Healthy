using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Application.Dispatchers;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Core.Contracts.Commands.Users;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            command.BindId(c => c.Id);
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
