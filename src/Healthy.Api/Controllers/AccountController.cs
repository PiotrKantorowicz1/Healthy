using System.Threading.Tasks;
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
        
        [HttpPost("name")]
        public async Task<IActionResult> Post(ChangeUsername command)
        {
            await DispatchAsync(command);
            return NoContent();
        }      

        [HttpPost("sign-up")]
        [AllowAnonymous]
        public async Task<IActionResult> Post(SignUp command)
        {
            await DispatchAsync(command);
            return NoContent();
        }

        [HttpPost("activate")]
        public async Task<IActionResult> Post(ActivateAccount command)
        {
            await DispatchAsync(command);
            return NoContent();
        }     
        
        [HttpPost("password")]
        public async Task<IActionResult> Post(ChangePassword command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
             
        [HttpPost("account")]
        public async Task<IActionResult> Post(DeleteAccount command)
        {
            await DispatchAsync(command);
            return NoContent();
        }
    }
}