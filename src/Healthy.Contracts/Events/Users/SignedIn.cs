using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedIn : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string UserId { get; }
        public string Email { get; }
        public string Name { get; }
        public string Provider { get; }
        
        public SignedIn(string userId, string email, 
            string name, string provider)
        {
            UserId = userId;
            Email = email;
            Name = name;
            Provider = provider;
        }
    }
}