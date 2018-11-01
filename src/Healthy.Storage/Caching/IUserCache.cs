using System;
using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.Storage.Caching
{
    public interface IUserCache
    {
        Task AddAsync(User user);
        Task DeleteAsync(string userId);
        Task AddRemarkAsync(string userId, Guid remarkId);
        Task DeleteRemarkAsync(string userId, Guid remarkId);                    
    }
}