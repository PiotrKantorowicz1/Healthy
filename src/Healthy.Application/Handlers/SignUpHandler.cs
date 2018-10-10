using Healthy.Application.Services.Users.Abstract;
using Healthy.Infrastructure.Handlers;
using System;
using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Extensions;

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
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, "Error occured while signing up a user");
                })
                .ExecuteAsync();
        }
    }
}