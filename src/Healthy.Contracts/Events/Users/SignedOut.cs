using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedOut : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        public Guid SessionId { get; }
        
        public SignedOut(Guid userId, Guid sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }
    }
}