using System;
using System.Threading.Tasks;
using Healthy.Core.Types;
using Healthy.Core.Domain.Users;

namespace Healthy.Core.Repositories
{
    public interface IOneTimeSecuredOperationRepository
    {
        Task<Maybe<OneTimeSecuredOperation>> GetAsync(Guid id);
        Task<Maybe<OneTimeSecuredOperation>> GetAsync(string type, string user, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        Task UpdateAsync(OneTimeSecuredOperation operation);
    }
}
