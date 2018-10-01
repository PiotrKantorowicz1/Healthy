using System.Collections.Generic;
using Healthy.Contracts.Events;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IAggregateRoot : IEntity
    {
        AggregateId AggregateId { get; }
        IEnumerable<IEvent> Events { get; }
        int Version { get; }
    }
}