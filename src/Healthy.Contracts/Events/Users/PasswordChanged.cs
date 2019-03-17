using System;

namespace Healthy.Contracts.Events.Users
{
    public class PasswordChanged : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        
        public PasswordChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}