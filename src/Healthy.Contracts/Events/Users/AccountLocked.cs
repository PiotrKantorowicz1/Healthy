using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountLocked : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        
        public AccountLocked(Guid userId)
        {
            UserId = userId;
        }
    }
}
