using System;

namespace Healthy.Contracts.Commands.Users
{
    public class RefreshUserSession : ICommand
    {
        public Guid SessionId { get; }
        public Guid NewSessionId { get; }
        public string Key { get; }
        
        public RefreshUserSession(string key, Guid sessionId, Guid newSessionId)
        {
            SessionId = sessionId;
            NewSessionId = newSessionId;
            Key = key;
        }
    }
}