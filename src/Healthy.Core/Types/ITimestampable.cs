using System;

namespace Healthy.Core.Types
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}