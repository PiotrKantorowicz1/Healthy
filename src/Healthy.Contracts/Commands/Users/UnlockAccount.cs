using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class UnlockAccount : ICommand
    {
        public string UserId { get; }
        public string UnlockUserId { get; }
        
        public UnlockAccount(string userId, string unlockUserId)
        {
            UserId = userId;
            UnlockUserId = unlockUserId;
        }
    }
}