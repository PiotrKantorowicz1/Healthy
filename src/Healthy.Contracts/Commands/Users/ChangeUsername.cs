using System;

namespace Healthy.Contracts.Commands.Users
{
    public class ChangeUsername : ICommand
    {
        public Guid UserId { get; }
        public string Name { get; }
        
        public ChangeUsername(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}