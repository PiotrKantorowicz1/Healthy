using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Application.Dispatchers;
using Healthy.Contracts.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            await DispatchAsync(command.BindId(c => c.Id));

            return NoContent();
        }
    }
}