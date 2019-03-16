using System;

namespace Healthy.Contracts.Events.Users
{
    public class PasswordChanged : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string UserId { get; }
        
        public PasswordChanged(string userId)
        {
            UserId = userId;
        }
    }
}