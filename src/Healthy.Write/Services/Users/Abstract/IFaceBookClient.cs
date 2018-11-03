using System.Threading.Tasks;

namespace Healthy.Write.Services.Users.Abstract
{
    public interface IFacebookClient : IService
    {
        Task<T> GetAsync<T>(string endpoint, string accessToken, string args = null);
        Task PostAsync(string endpoint, string accessToken, dynamic data, string args = null);
    }
}