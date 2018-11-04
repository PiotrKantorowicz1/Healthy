using Healthy.Infrastructure.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok("Welcome in Healthy application");
    }
}