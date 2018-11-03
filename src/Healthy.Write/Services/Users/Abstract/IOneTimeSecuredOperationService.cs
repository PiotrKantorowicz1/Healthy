using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Types;

namespace Healthy.Write.Services.Users.Abstract
{
    public interface IOneTimeSecuredOperationService : IService
    {
        Task<Maybe<OneTimeSecuredOperation>> GetAsync(Guid id);
        Task CreateAsync(Guid id, string type, string user, DateTime expiry);
        Task<bool> CanBeConsumedAsync(string type, string user, string token);
        Task ConsumeAsync(string type, string user, string token);
    }
}