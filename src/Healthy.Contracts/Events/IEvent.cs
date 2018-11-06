using System;

namespace Healthy.Contracts.Events
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}