using System;

namespace Healthy.Contracts.Events.Users
{
    public class AccountUnlocked : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public AccountUnlocked()
        {

        }
    }
}