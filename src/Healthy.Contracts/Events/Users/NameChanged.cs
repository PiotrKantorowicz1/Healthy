using System;

namespace Healthy.Contracts.Events.Users
{
    public class NameChanged : IEvent
    {
        public Guid Id => Guid.NewGuid();
        public string Name { get; }
        
        public NameChanged(string name)
        {
            Name = name;
        }
    }
}