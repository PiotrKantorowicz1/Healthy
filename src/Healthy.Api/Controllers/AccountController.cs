using Healthy.Application.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

    }
}
