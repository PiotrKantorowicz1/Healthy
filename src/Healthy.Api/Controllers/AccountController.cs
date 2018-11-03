using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Dispatchers;
using Healthy.Read.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(GetUser query)
            => Single(await QueryAsync(query));

        [HttpGet("names/{name}/available")]
        public async Task<IActionResult> Get(GetNameAvailability query)
            => Single(await QueryAsync(query));
        
        [HttpGet("state")]
        public async Task<IActionResult> Get(GetUserState query)
            => Single(await QueryAsync(query));
        
        [HttpPost("name")]
        public async Task<IActionResult> Post(ChangeUsername command)
        {
            await DispatchAsync(command.Bind(c => c.UserId, UserId));
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
            await DispatchAsync(command.Bind(c => c.UserId, UserId));
            return NoContent();
        }
             
        [HttpPost("account")]
        public async Task<IActionResult> Post(DeleteAccount command)
        {
            await DispatchAsync(command.Bind(c => c.UserId, UserId));
            return NoContent();
        }
    }
}