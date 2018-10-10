using Healthy.Application.Dispatchers;

namespace Healthy.Api.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

    }
}
