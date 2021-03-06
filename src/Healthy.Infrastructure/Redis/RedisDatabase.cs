using StackExchange.Redis;

namespace Healthy.Infrastructure.Redis
{
    public class RedisDatabase
    {
        public IDatabase Database { get; }

        public RedisDatabase(IDatabase database)
        {
            Database = database;
        }
    }
}