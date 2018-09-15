using System;
using Healthy.Core.Base;

namespace Healthy.Core.Entities.Users
{
    public class User : Entity, ITimestampable
    {
        public Avatar Avatar { get; protected set; }
        public string UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Provider { get; protected set; }
        public string Role { get; protected set; }
        public string State { get; protected set; }
        public string ExternalUserId { get; protected set; }
        public string Culture { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
    }
}