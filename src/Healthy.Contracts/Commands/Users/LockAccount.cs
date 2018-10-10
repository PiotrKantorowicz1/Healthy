namespace Healthy.Contracts.Commands.Users
{
    public class LockAccount : ICommand
    {
        public string UserId { get; }
        public string LockUserId { get; }
        
        public LockAccount(string userId, string lockUserId)
        {
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}