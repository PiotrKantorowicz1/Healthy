using System;

namespace Healthy.Read.Dtos.Users
{
    public class UserInfoDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}