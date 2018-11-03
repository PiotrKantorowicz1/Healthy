using System.Threading.Tasks;
using Healthy.Api.Framework.Extensions;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Dispatchers;
using Healthy.Read.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Healthy.Api.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> Get(BrowseUsers query)
            => Collection(await QueryAsync(query));
        
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(GetUserInfoByName query)
            => Single(await QueryAsync(query));

        [HttpPut("{lockUserId}/lock")]
        public async Task<IActionResult> Put(string lockUserId, LockAccount command)
        {
            await DispatchAsync(command.Bind(c => c.UserId, lockUserId));
            return NoContent();
        }
        
        [HttpPut("{unlockUserId}/unlock")]
        public async Task<IActionResult> Put(string unlockUserId, UnlockAccount command)
        {
            await DispatchAsync(command.Bind(c => c.UnlockUserId, unlockUserId));
            return NoContent();
        }        
    }
}
