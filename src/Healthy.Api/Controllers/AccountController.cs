using Healthy.Application.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }
    }
}



