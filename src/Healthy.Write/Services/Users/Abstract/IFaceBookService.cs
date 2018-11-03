using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Types;

namespace Healthy.Write.Services.Users.Abstract
{
    public interface IFacebookService : IService
    {
        Task<Maybe<FacebookUser>> GetUserAsync(string accessToken);
        Task<bool> ValidateTokenAsync(string accessToken);
        Task PostOnWallAsync(string accessToken, string message);
        string GetAvatarUrl(string facebookId);
    }
}