using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class SetNewPassword : ICommand
    {
        public string Email { get; }
        public string Token { get; }
        public string Password { get; }
        
        public SetNewPassword(string email, string token, string password)
        {
            Email = email;
            Token = token;
            Password = password;
        }
    }
}