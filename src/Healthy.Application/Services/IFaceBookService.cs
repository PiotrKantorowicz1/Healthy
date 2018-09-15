using System.Threading.Tasks;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Types;

namespace Healthy.Application.Services
{
    public interface IFacebookService
    {
        Task<Maybe<FacebookUser>> GetUserAsync(string accessToken);
        Task<bool> ValidateTokenAsync(string accessToken);
        Task PostOnWallAsync(string accessToken, string message);
        string GetAvatarUrl(string facebookId);
    }
}