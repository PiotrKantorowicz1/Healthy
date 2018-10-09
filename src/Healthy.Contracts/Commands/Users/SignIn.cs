using System;

namespace Healthy.Contracts.Commands.Users
{
    public class SignIn : ICommand
    {
        public Guid SessionId { get; }
        public string Email { get; }
        public string Password { get; }
        public string IpAddress { get; }
        public string UserAgent { get; }
        public string AccessToken { get; }
        public string Provider { get; }
        
        public SignIn(Guid sessionId, string email, string password, string ipAddress,
            string userAgent, string accessToken, string provider)
        {
            SessionId = sessionId;
            Email = email;
            Password = password;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            AccessToken = accessToken;
            Provider = provider;
        }
    }
}