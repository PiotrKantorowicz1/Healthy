using System.Threading.Tasks;

namespace Healthy.Write.Services.Users.Abstract
{
    public interface IAccessTokenService : IService
    {
        Task<bool> IsCurrentActiveToken();
        Task DeactivateCurrentAsync(string userId);
        Task<bool> IsActiveAsync(string token);
        Task DeactivateAsync(string userId, string token);
    }
}