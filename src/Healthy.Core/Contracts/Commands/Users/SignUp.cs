namespace Healthy.Core.Contracts.Commands.Users
{
    public class SignUp : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public string AccessToken { get; set; }
        public string Provider { get; set; }
    }
}