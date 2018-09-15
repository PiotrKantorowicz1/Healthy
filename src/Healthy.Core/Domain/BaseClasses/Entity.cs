using System;

namespace Healthy.Core.Domain.BaseClasses
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}