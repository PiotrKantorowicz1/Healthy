using System;
using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;
using Healthy.Contracts.Commands;
using Healthy.Contracts.Commands.Users;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Application.Handlers.Users
{
    public class RemoveAvatarHandler : ICommandHandler<RemoveAvatar>
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