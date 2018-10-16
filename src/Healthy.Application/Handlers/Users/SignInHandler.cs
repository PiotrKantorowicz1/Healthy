using System;
using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands.Users;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Types;
using Healthy.Infrastructure.Handlers;
using Serilog;

namespace Healthy.Application.Handlers.Users
{
    public class SignInHandler : ICommandHandler<SignIn>
    {
        private static readonly ILogger Logger = Log.Logger;

        private readonly IHandler _handler;
        private readonly IUserService _userService;
        private readonly IFacebookService _facebookService;
        private readonly IAuthenticationService _authenticationService;

        public SignInHandler(IHandler handler,
            IUserService userService,
            IFacebookService facebookService,
            IAuthenticationService authenticationService)
        {
            _handler = handler;
            _userService = userService;
            _facebookService = facebookService;
            _authenticationService = authenticationService;
        }

        public async Task HandleAsync(SignIn command)
        {
            Maybe<User> user = null;
            await _handler
                .Run(async () =>
                {
                    switch (command.Provider?.ToLowerInvariant())
                    {
                        case "healthy":
                            user = await HandleDefaultSignInAsync(command);
                            break;
                        case "facebook":
                            //user = await HandleFacebookSignInAsync(command);
                            break;
                        default:
                            throw new ArgumentException($"Invalid provider: {command.Provider}", nameof(command.Provider));
                    }
                })
                .OnError((ex, logger) =>
                {
                    Logger.Error(ex, "Error occured while signing in");
                })
                .ExecuteAsync();
        }

        private async Task<Maybe<User>> HandleDefaultSignInAsync(SignIn command)
        {
            await _authenticationService.SignInAsync(command.SessionId,
                command.Email, command.Password, command.IpAddress, command.UserAgent);

            return await _userService.GetByEmailAsync(command.Email, command.Provider);
        }
    }
}