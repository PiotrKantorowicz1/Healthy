using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountActivated : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public AccountActivated()
        {

        }
    }
}