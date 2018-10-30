using System;
using System.Threading.Tasks;
using Healthy.ReadSide.Models;

namespace Healthy.ReadSide.Services
{
    public interface IUserCache
    {
        Task AddAsync(UserRM user);
        Task DeleteAsync(string userId);
        Task AddRemarkAsync(string userId, Guid remarkId);
        Task DeleteRemarkAsync(string userId, Guid remarkId);                    
    }
}