using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedOut : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public SignedOut()
        {

        }
    }
}