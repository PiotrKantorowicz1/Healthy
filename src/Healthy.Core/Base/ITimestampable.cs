using System;

namespace Healthy.Core.Base
{
    public interface ITimestampable
    {
        DateTime CreatedAt { get; }
    }
}