using System;
using System.Threading.Tasks;
using Healthy.Core.Types;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Core.Domain.Users.Repositories
{
    public interface IOneTimeSecuredOperationRepository : IRepository
    {
        Task<Maybe<OneTimeSecuredOperation>> GetAsync(Guid id);
        Task<Maybe<OneTimeSecuredOperation>> GetAsync(string type, string user, string token);
        Task AddAsync(OneTimeSecuredOperation operation);
        Task UpdateAsync(OneTimeSecuredOperation operation);
    }
}
