using System;

namespace Healthy.Core.Contracts.Commands.Users
{
    public class SignIn : ICommand
    {
        public Guid SessionId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string AccessToken { get; set; }
        public string Provider { get; set; }
    }
}