using System.Threading.Tasks;
using Healthy.Infrastructure.Files;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IAvatarService : IService
    {
        Task<string> GetUrlAsync(string userId);
        Task AddOrUpdateAsync(string userId, File avatar);
        Task RemoveAsync(string userId);
    }
}