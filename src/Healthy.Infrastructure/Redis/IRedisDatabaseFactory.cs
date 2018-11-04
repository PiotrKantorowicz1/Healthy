using Healthy.Core.Types;

namespace Healthy.Infrastructure.Redis
{
    public interface IRedisDatabaseFactory
    {
        Maybe<RedisDatabase> GetDatabase(int id = -1);
    }
}