using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Application.Dispatchers;
using Healthy.Contracts.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            command.BindId(c => c.Id);
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
