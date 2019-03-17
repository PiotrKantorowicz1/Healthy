using System;
using System.Threading.Tasks;

namespace Healthy.Services.Services.Users.Abstract
{
    public interface IAccessTokenService : IService
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync(Guid userId);
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(Guid userId, string token);
    }
}