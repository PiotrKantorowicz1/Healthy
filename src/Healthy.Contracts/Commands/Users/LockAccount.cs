using System;

namespace Healthy.Contracts.Commands.Users
{
    public class LockAccount : ICommand
    {
        public Guid UserId { get; }
        public Guid LockUserId { get; }
        
        public LockAccount(Guid userId, Guid lockUserId)
        {
            UserId = userId;
            LockUserId = lockUserId;
        }
    }
}