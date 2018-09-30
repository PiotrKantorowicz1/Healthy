using System.Collections.Generic;
using Healthy.Core.Domain.Shared.DomainClasses;

namespace Healthy.Core.Domain.BaseClasses
{
    public interface IScorable
    {
        int Rating { get; }
        IEnumerable<Vote> Votes { get; }
    }
}