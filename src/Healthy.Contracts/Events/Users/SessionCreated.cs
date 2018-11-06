using System;

namespace Healthy.Contracts.Events.Users
{
    public class SessionCreated : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public SessionCreated()
        {

        }
    }
}