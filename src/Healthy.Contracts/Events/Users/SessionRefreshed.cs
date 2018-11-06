using System;

namespace Healthy.Contracts.Events.Users
{
    public class SessionRefreshed : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public SessionRefreshed()
        {

        }
    }
}
