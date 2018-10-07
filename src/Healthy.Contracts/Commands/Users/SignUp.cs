using System;

namespace Healthy.Contracts.Commands.Users
{
    public class SignUp : ICommand
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Password { get; }
        public string Name { get; }
        public string Role { get; }
        public string State { get; }
        public string AccessToken { get; }
        public string Provider { get; }
        
        public SignUp(Guid id, string email, string password, string name, 
            string role, string state, string accessToken, string provider)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            Role = role;
            State = state;
            AccessToken = accessToken;
            Provider = provider;
        }
    }
}