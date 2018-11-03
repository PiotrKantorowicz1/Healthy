using System.Collections.Generic;
using System.Threading.Tasks;
using Healthy.Write.Services.Users.Abstract;

namespace Healthy.Write.Services.Users
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(string userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}