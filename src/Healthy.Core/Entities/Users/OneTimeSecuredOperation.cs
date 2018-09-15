using System;
using Healthy.Core.Base;

namespace Healthy.Core.Entities.Users
{
    public class OneTimeSecuredOperation : Entity, ITimestampable
    {
        public string Type { get; protected set; }
        public string User { get; protected set; }
        public string Token { get; protected set; }
        public string RequesterIpAddress { get; protected set; }
        public string RequesterUserAgent { get; protected set; }
        public string ConsumerIpAddress { get; protected set; }
        public string ConsumerUserAgent { get; protected set; }
        public bool Consumed => ConsumedAt.HasValue;
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ConsumedAt { get; protected set; }
        public DateTime Expiry { get; protected set; }
    }
}