using System.Threading.Tasks;
using Healthy.Core.Domain.Users.DomainClasses;

namespace Healthy.EventStore.Caching
{
    public interface IUserCache : ICacheMarker
    {
        Task AddAsync(User user);
        Task DeleteAsync(string userId);
    }
}