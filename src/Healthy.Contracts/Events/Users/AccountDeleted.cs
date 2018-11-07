using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountDeleted : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string UserId { get; }
        
        public AccountDeleted(string userId)
        {
            UserId = userId;
        }
    }
}