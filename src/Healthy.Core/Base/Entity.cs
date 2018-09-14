using System;

namespace Healthy.Core.Base
{
    public class Entity : IEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();

        protected Entity()
        {
        }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}