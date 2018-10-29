using Healthy.Contracts.Events;

namespace Healthy.Contracts.Events.Users
{
    public class SignedUp : IEvent
    {
        public string UserId { get; }
        public string Provider { get; }
        public string Role { get; }
        public string State { get; }
        
        public SignedUp(string userId, string provider, 
            string role, string state)
        {
            UserId = userId;
            Provider = provider;
            Role = role;
            State = state;
        }
    }
}