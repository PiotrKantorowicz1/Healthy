using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Dispatchers;
using Healthy.Read.Queries.Users;
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
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]GetUser query)
            => Single(await QueryAsync(query));

        [HttpGet("names/{name}/available")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute]GetNameAvailability query)
            => Single(await QueryAsync(query));
        
        [HttpGet("state")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]GetUserState query)
            => Single(await QueryAsync(query));
        
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