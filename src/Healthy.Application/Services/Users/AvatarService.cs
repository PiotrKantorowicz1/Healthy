using System.IO;
using System.Threading.Tasks;
using Healthy.Application.Services.Users.Abstract;

namespace Healthy.Application.Services.Users
{
    public class AvatarService : IAvatarService
    {
        public Task AddOrUpdateAsync(string userId, int avatar)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUrlAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}