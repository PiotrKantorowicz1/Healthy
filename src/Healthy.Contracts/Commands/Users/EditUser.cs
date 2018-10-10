using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class EditUser : ICommand
    {
        public string UserId { get; }
        public string Email { get; }
        public string Name { get; }
        
        public EditUser(string userId, string email, string name)
        {
            UserId = userId;
            Email = email;
            Name = name;
        }
    }
}