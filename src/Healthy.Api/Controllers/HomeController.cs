using Healthy.Application.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok("Welcome in Healthy application");
    }
}