using System;
using System.Threading.Tasks;
using Healthy.Core.Types;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Core.Domain.BaseClasses;

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