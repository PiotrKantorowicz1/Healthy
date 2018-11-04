using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Services.Handlers.Users
{
    public sealed class PostOnFacebookWallHandler : ICommandHandler<PostOnFacebookWall>
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