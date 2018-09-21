using Healthy.Application.Services.Users.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthy.Application.Services.Users
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(string userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}