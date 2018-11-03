using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class ResetPasswordController : BaseController
    {
        public ResetPasswordController(IDispatcher dispatcher) : base(dispatcher)
        {
        }
        
        [HttpPost("set-new")]
        public async Task<IActionResult> Post(SetNewPassword command)
        {
            await DispatchAsync(command);
            return NoContent();
        }     
    }
}