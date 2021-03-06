using System.Collections.Generic;
using Healthy.Core.Domain.Shared.ValueObjects;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IScorable
    {
        int Rating { get; }
        IEnumerable<Vote> Votes { get; }
    }
}