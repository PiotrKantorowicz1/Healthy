using System;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}