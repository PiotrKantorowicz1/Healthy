using System;

namespace Healthy.Contracts.Commands.Users
{
    public class UnlockAccount : ICommand
    {
        public Guid UserId { get; }
        public Guid UnlockUserId { get; }
        
        public UnlockAccount(Guid userId, Guid unlockUserId)
        {
            UserId = userId;
            UnlockUserId = unlockUserId;
        }
    }
}