using System;
using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Application.Handlers
{
    public class PostOnFacebookWallHandler : ICommandHandler<PostOnFacebookWall>
    {
        private readonly IHandler _handler;
        private readonly IFacebookService _facebookService;

        public PostOnFacebookWallHandler(IHandler handler,
            IFacebookService facebookService)
        {
            _handler = handler;
            _facebookService = facebookService;
        }

        public async Task HandleAsync(PostOnFacebookWall command)
            => await _handler
                .Run(async () => await _facebookService.PostOnWallAsync(command.AccessToken, command.Message))
                .OnError((ex, logger) => logger.Error(ex, "Error occured while posting message on facebook wall"))
                .ExecuteAsync();
    }
}