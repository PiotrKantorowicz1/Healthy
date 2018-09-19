using System;
using System.Collections.Generic;
using System.Text;

namespace Healthy.Infrastructure.Settings
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; }
        public string Instance { get; set; }
    }
}
