﻿namespace Healthy.Infrastructure.Settings
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; }
        public string Instance { get; set; }
        public int Database { get; set; }
        public bool Enabled { get; set; }        
    }
}