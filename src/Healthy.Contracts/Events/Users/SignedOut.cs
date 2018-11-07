using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedOut : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string UserId { get; }
        public Guid SessionId { get; }
        
        public SignedOut(string userId, Guid sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }
    }
}