using Healthy.Contracts.Commands.Models;
using System;

namespace Healthy.Contracts.Commands.Users
{
    public class UploadAvatar : ICommand
    {
        public Guid UserId { get; }
        public File Avatar { get; }

        public UploadAvatar(Guid userId, File avatar)
        {
            UserId = userId;
            Avatar = avatar;
        }
    }
}