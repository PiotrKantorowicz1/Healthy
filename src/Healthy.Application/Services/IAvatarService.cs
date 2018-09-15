using System.IO;
using System.Threading.Tasks;

namespace Healthy.Application.Services
{
    public interface IAvatarService
    {
         Task<string> GetUrlAsync(string userId);
         Task AddOrUpdateAsync(string userId, File avatar);
         Task RemoveAsync(string userId);
    }
}