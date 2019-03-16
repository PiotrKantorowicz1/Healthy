using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountLocked : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string UserId { get; }
        
        public AccountLocked(string userId)
        {
            UserId = userId;
        }
    }
}
