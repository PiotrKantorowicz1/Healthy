using System;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}