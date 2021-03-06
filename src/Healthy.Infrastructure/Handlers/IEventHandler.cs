using System.Threading.Tasks;
using Healthy.Contracts.Events;

namespace Healthy.Infrastructure.Handlers
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}