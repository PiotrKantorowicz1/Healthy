using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountLocked : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public AccountLocked()
        {

        }
    }
}
