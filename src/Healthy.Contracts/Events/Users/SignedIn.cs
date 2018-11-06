using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedIn : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public SignedIn()
        {

        }
    }
}