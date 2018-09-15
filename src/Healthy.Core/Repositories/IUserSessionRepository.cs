using System;
using System.Threading.Tasks;
using Healthy.Core.Types;
using Healthy.Core.Domain.Users;

namespace Healthy.Core.Repositories
{
    public interface IUserSessionRepository
    {
        Task<Maybe<UserSession>> GetByIdAsync(Guid id);
        Task AddAsync(UserSession session);
        Task UpdateAsync(UserSession session);
        Task DeleteAsync(Guid id);
    }
}