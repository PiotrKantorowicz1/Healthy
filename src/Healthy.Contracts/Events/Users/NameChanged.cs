using System;

namespace Healthy.Contracts.Events.Users
{
    public class NameChanged : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }
        public string NewName { get; }
        public string State { get; }
        
        public NameChanged(Guid userId, string newName, string state)
        {
            UserId = userId;
            NewName = newName;
            State = state;
        }
    }
}