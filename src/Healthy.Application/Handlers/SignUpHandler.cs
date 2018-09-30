using Healthy.Application.Services.Users.Abstract;
using Healthy.Core.Contracts.Commands.Users;
using Healthy.Core.Extensions;
using Healthy.Infrastructure.Handlers;
using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Application.Handlers
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public SignUpHandler(IHandler handler, IUserService userService)
        {
            _handler = handler;
            _userService = userService;
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
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, "Error occured while signing up a user");
                })
                .ExecuteAsync();
        }
    }
}