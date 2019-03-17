using System;

namespace Healthy.Contracts.Events.Users
{
    public class RoleChanged
    {
        public Guid Id => Guid.NewGuid();
        public Guid UserId { get; }

        public RoleChanged(Guid userId)
        {
            UserId = userId;
        }
    }
}
