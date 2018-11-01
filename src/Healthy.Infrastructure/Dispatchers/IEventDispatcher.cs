using System.Threading.Tasks;
using Healthy.Contracts.Events;

namespace Healthy.Infrastructure.Dispatchers
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(params IEvent[] events);
    }
}