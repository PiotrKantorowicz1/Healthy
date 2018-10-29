using Healthy.Contracts.Events;

namespace Healthy.Contracts.Events.Users
{
    public class SignUpRejected : IEvent
    {
        public string UserId { get; }
        public string Code { get; }
        public string Reason { get; }
        public string Provider { get; }
        
        public SignUpRejected(string userId, string code, 
            string reason, string provider)
        {
            UserId = userId;
            Code = code;
            Reason = reason;
            Provider = provider;
        }
    }
}