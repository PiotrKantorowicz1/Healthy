using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Handlers.Users
{
    public sealed class RemoveAvatarHandler : ICommandHandler<RemoveAvatar>
    {
        private readonly IHandler _handler;
        private readonly IAvatarService _avatarService;

        public RemoveAvatarHandler(IHandler handler,
            IAvatarService avatarService)
        {
            _handler = handler;
            _avatarService = avatarService;
        }

        public async Task HandleAsync(RemoveAvatar command)
            => await _handler
                .Run(async () => await _avatarService.RemoveAsync(command.UserId))
                .OnError((ex, logger) =>
                    logger.Error(ex, "Error occured while removing avatar."))
                .ExecuteAsync();
    }
}