using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountDeleted : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        
        public AccountDeleted(Guid userId)
        {
            UserId = userId;
        }
    }
}