using System;
using Healthy.Core.Base;

namespace Healthy.Core.Entities.Users
{
    public class UserSession : ITimestampable
    {
        public Guid Id { get; protected set; }
        public string UserId { get; protected set; }
        public string Key { get; protected set; }
        public string UserAgent { get; protected set; }
        public string IpAddress { get; protected set; }
        public Guid? ParentId { get; protected set; }
        public bool Refreshed { get; protected set; }
        public bool Destroyed { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
    }
}