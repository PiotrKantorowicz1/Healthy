using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Application.Dispatchers;
using Healthy.Contracts.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

    }
}
