using System.IO;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IAvatarService
    {
         Task<string> GetUrlAsync(string userId);
         Task AddOrUpdateAsync(string userId, int avatar);
         Task RemoveAsync(string userId);
    }
}