using System.Collections.Generic;

namespace Healthy.Infrastructure.Security
{
    public class JsonWebTokenPayload
    {
        public string Subject { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public long Expires { get; set; }
        public IDictionary<string, string> Claims { get; set; }
    }
}
