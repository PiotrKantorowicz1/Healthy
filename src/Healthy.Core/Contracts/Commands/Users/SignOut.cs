using System;

namespace Healthy.Core.Contracts.Commands.Users
{
    public class SignOut : ICommand
    {
        public Guid SessionId { get; }
        public string UserId { get; }
        
        public SignOut(Guid sessionId, string userId)
        {
            SessionId = sessionId;
            UserId = userId;
        }
    }
}