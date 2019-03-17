using System;

namespace Healthy.Contracts.Commands.Users
{
    public class DeleteAccount : ICommand
    {
        public Guid UserId { get; }
        public bool Soft { get; }
        
        public DeleteAccount(Guid userId, bool soft)
        {
            UserId = userId;
            Soft = soft;
        }
    }
}