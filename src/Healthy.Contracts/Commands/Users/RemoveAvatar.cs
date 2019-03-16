using System;

namespace Healthy.Contracts.Commands.Users
{
    public class RemoveAvatar : ICommand
    {
        public Guid UserId { get; }
        
        public RemoveAvatar(Guid userId)
        {
            UserId = userId;
        }
    }
}