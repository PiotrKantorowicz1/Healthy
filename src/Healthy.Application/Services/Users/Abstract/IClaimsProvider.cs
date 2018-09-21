using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users.Abstract
{
    public interface IClaimsProvider : IService
    {
        Task<IDictionary<string, string>> GetAsync(string userId);
    }
}