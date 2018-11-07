using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountActivated : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string UserId { get; }
        public string Email { get; }
        
        public AccountActivated(string email, string userId)
        {
            UserId = userId;
            Email = email;
        }
    }
}