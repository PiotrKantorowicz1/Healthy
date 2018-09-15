using System;

namespace Healthy.Core.Domain
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}