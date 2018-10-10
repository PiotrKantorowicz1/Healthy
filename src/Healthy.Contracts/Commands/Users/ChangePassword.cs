using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class ChangePassword : ICommand
    {
        public string UserId { get; }
        public string CurrentPassword { get; }
        public string NewPassword { get; }
        
        public ChangePassword(string userId, string currentPassword, string newPassword)
        {
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}