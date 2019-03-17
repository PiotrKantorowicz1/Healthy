using System;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IEditable
    {
        DateTime UpdatedAt { get; }
    }
}
