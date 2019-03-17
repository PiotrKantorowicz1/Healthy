using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedUp : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        public string Provider { get; }
        public string Role { get; }

        public SignedUp(Guid userId, string provider, string role)
        {
            UserId = userId;
            Provider = provider;
            Role = role;
        }
    }
}