using System;
using System.Threading.Tasks;
using Healthy.Core.Types;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Core.Domain.Users.Repositories
{
    public interface IUserSessionRepository : IRepository
    {
        Task<Maybe<UserSession>> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        Task UpdateAsync(UserSession session);
        Task DeleteAsync(Guid id);
    }
}