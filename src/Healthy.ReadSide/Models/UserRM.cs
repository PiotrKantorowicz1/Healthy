using System;

namespace Healthy.ReadSide.Models
{
    public class UserRM
    {
        public AvatarRM Avatar { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Provider { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public string ExternalUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}