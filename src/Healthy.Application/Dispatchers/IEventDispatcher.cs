using System.Threading.Tasks;
using Healthy.Contracts.Events;

namespace Healthy.Application.Dispatchers
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(params IEvent[] events);
    }
}