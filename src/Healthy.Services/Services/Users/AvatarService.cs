using System.Threading.Tasks;
using Healthy.Core;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;
using Healthy.Infrastructure.Files;
using Healthy.Services.Services.Users.Abstract;
using Serilog;

namespace Healthy.Services.Services.Users
{
    public class AvatarService : IAvatarService
    {
        private static readonly ILogger Logger = Log.Logger;
        private readonly IUserRepository _userRepository;
        private readonly IFileHandler _fileHandler;
        private readonly IImageService _imageService;
        private readonly IFileValidator _fileValidator;

        public AvatarService(IUserRepository userRepository,
            IFileHandler fileHandler, IImageService imageService,
            IFileValidator fileValidator)
        {
            _userRepository = userRepository;
            _fileHandler = fileHandler;
            _imageService = imageService;
            _fileValidator = fileValidator;
        }

        public async Task<string> GetUrlAsync(string userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }

            return user.Value.Avatar?.Url ?? string.Empty;
        }

        public async Task AddOrUpdateAsync(string userId, File avatar)
        {
            if (avatar == null)
            {
                throw new ServiceException(ErrorCodes.InvalidFile,
                    $"There is no avatar file to be uploaded.");
            }

            if (!_fileValidator.IsImage(avatar))
            {
                throw new ServiceException(ErrorCodes.InvalidFile);
            }

            var user = await _userRepository.GetByUserIdAsync(userId);
            var name = $"avatar_{userId:N}.jpg";
            var resizedAvatar = _imageService.ProcessImage(avatar, 200);
            await RemoveAsync(user, userId);
            await _fileHandler.UploadAsync(resizedAvatar, name,
                (baseUrl, fullUrl) => { user.Value.SetAvatar(Avatar.Create(name, fullUrl)); });
            await _userRepository.UpdateAsync(user.Value);
        }

        public async Task RemoveAsync(string userId)
        {
            var user = await _userRepository.GetByUserIdAsync(userId);
            await RemoveAsync(user, userId);
            await _userRepository.UpdateAsync(user.Value);
        }

        private async Task RemoveAsync(Maybe<User> user, string userId)
        {
            if (user.HasNoValue)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User with id: '{userId}' has not been found.");
            }

            if (user.Value.Avatar == null)
            {
                return;
            }

            if (user.Value.Avatar.IsEmpty)
            {
                return;
            }

            await _fileHandler.DeleteAsync(user.Value.Avatar.Name);
            user.Value.RemoveAvatar();
        }
    }
}