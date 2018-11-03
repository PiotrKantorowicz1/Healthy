using System.Threading.Tasks;
using Healthy.Contracts.Commands.Users;
using Healthy.Core;
using Healthy.Core.Exceptions;
using Healthy.Infrastructure.Files;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Handlers.Users
{
    public sealed class UploadAvatarHandler : ICommandHandler<UploadAvatar>
    {
        private readonly IHandler _handler;
        private readonly IAvatarService _avatarService;
        private readonly IFileResolver _fileResolver;

        public UploadAvatarHandler(IHandler handler,
            IAvatarService avatarService,
            IFileResolver fileResolver)
        {
            _handler = handler;
            _avatarService = avatarService;
            _fileResolver = fileResolver;
        }

        public async Task HandleAsync(UploadAvatar command)
        {
            await _handler
                .Run(async () =>
                {
                    var avatar = _fileResolver.FromBase64(command.Avatar.Base64, command.Avatar.Name,
                        command.Avatar.ContentType);
                    if (avatar.HasNoValue)
                    {
                        throw new ServiceException(ErrorCodes.InvalidAvatar);
                    }

                    await _avatarService.AddOrUpdateAsync(command.UserId, avatar.Value);
                })
                .OnError((ex, logger) =>
                    logger.Error(ex, "Error occured while uploading avatar."))
                .ExecuteAsync();
        }
    }
}