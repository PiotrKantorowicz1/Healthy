using System;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IAggregateRoot 
    {
        Guid Id { get; }
    }
}