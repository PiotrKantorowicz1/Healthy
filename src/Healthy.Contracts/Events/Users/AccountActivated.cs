using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountActivated : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        public string Email { get; }
        
        public AccountActivated(string email, Guid userId)
        {
            UserId = userId;
            Email = email;
        }
    }
}