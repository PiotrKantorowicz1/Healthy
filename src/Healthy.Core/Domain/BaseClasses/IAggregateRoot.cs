using System;
using System.Collections.Generic;
using Healthy.Contracts.Events;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IAggregateRoot 
    {
        Guid Id { get; }
    }
}