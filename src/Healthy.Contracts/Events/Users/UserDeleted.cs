using System;

namespace Healthy.Contracts.Events.Users
{
    public class UserDeleted : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public UserDeleted()
        {

        }
    }
}