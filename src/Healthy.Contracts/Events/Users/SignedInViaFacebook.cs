using System;

namespace Healthy.Contracts.Events.Users
{
    public class SignedInViaFacebook : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public SignedInViaFacebook()
        {

        }
    }
}