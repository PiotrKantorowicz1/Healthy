using System.Threading.Tasks;
using Healthy.Contracts.Events;

namespace Healthy.Infrastructure.Dispatchers
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}