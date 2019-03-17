using System;
using System.Threading.Tasks;
using Healthy.Infrastructure.Files;

namespace Healthy.Services.Services.Users.Abstract
{
    public interface IAvatarService : IService
    {
        Task<string> GetUrlAsync(Guid userId);
        Task AddOrUpdateAsync(Guid userId, File avatar);
        Task RemoveAsync(Guid userId);
    }
}