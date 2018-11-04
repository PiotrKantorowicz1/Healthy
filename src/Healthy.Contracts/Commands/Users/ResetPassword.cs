namespace Healthy.Contracts.Commands.Users
{
    public class ResetPassword : ICommand
    {
        public string Email { get; }
        public string Endpoint { get; }
        
        public ResetPassword(string email, string endpoint)
        {
            Email = email;
            Endpoint = endpoint;
        }
    }
}