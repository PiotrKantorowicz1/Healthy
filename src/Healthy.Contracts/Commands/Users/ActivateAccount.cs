namespace Healthy.Contracts.Commands.Users
{
    public class ActivateAccount : ICommand
    {
        public string Email { get; }
        public string Token { get; }
        
        public ActivateAccount(string email, string token)
        {
            Email = email;
            Token = token;
        }
    }
}