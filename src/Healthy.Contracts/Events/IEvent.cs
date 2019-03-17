using MediatR;
using System;

namespace Healthy.Contracts.Events
{
    public interface IEvent : INotification
    {
        Guid Id { get; }
    }
}