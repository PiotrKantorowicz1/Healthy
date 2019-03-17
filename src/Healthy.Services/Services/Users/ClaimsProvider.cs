using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Healthy.Services.Services.Users.Abstract;

namespace Healthy.Services.Services.Users
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}