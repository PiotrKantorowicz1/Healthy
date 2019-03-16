using System;

namespace Healthy.Contracts.Events.Users
{
    public class RemovedAvatar : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public RemovedAvatar()
        {

        }
    }
}