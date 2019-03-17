using System;

namespace Healthy.Contracts.Commands.Users
{
    public class SignOut : ICommand
    {
        public Guid SessionId { get; }      
        public Guid UserId { get; }
        
        public SignOut(Guid sessionId, Guid userId)
        {
            SessionId = sessionId;
            UserId = userId;
        }
    }
}