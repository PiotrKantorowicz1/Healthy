using Healthy.Application.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Extensions;
using Healthy.Infrastructure.Dispatchers;

namespace Healthy.Application.Handlers.Users
{
    public sealed class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;
        private readonly IEventDispatcher _eventDispatcher;

        public SignUpHandler(IHandler handler, 
            IUserService userService,
            IEventDispatcher eventDispatcher)
        {
            _handler = handler;
            _userService = userService;
            _eventDispatcher = eventDispatcher;
        }

        public async Task HandleAsync(SignUp command)
        {
            var userId = Guid.NewGuid().ToString("N");
            await _handler
                .Run(async () => await _userService.SignUpAsync(userId, command.Email,
                    command.Role.Empty() ? Roles.User : command.Role, Providers.Healthy,
                    password: command.Password, name: command.Name,
                    activate: command.State == "active"))
                .OnSuccess(async () =>
                {
                    var user = await _userService.GetAsync(userId);
                    await _eventDispatcher.DispatchAsync(user.Value.Events.ToArray());
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, "Error occured while signing up a user");
                })
                .ExecuteAsync();
        }
    }
}