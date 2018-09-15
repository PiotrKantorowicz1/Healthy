using System;

namespace Healthy.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}