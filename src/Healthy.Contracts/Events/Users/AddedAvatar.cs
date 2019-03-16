using System;

namespace Healthy.Contracts.Events.Users
{
    public class AddedAvatar : IEvent
    {
        public Guid Id => Guid.NewGuid();
        
        public AddedAvatar()
        {

        }
    }
}