using Healthy.Infrastructure.Handlers;
using System;
using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Core.Extensions;
using Healthy.Services.Services.Users.Abstract;
using Healthy.Core.Domain.Users.Enumerations;

namespace Healthy.Services.Handlers.Users
{
    public sealed class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;

        public SignUpHandler(IHandler handler, 
            IUserService userService)
        {
            _handler = handler;
            _userService = userService;
        }

        public async Task HandleAsync(SignUp command)
        {
            var userId = Guid.NewGuid();
            await _handler
                .Run(async () =>
                {
                    await _userService.SignUpAsync(command.Id, userId, command.Email,
                        command.Role.Empty() ? Roles.User.Name : command.Role, 
                        ProviderType.Healthy.Name, password: command.Password, 
                        name: command.Name, activate: command.State == "active");
                    
                })
                .OnError((ex, logger) =>
                {
                    logger.Error(ex, "Error occured while signing up a user");
                })
                .ExecuteAsync();
        }
    }
}