using System;

namespace Healthy.Infrastructure.Security
{
    public class JwtSession : JsonWebToken
    {
        public Guid SessionId { get; set; }
        public string Key { get; set; }
    }
}
