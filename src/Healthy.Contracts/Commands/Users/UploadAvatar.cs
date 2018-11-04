using Healthy.Contracts.Commands.Models;

namespace Healthy.Contracts.Commands.Users
{
    public class UploadAvatar : ICommand
    {
        public string UserId { get; }
        public File Avatar { get; }

        public UploadAvatar(string userId, File avatar)
        {
            UserId = userId;
            Avatar = avatar;
        }
    }
}