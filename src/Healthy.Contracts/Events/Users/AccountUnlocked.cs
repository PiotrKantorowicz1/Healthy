using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountUnlocked : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        
        public AccountUnlocked(Guid userId)
        {
            UserId = userId;
        }
    }
}