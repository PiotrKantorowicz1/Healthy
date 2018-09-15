using System;

namespace Healthy.Common.Extensions.cs
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid target) => target == Guid.Empty;
    }
}