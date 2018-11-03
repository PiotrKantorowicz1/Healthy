using System;
using System.Threading.Tasks;
using Autofac;
using Healthy.Contracts.Events;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Infrastructure.Dispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<TEvent>(params TEvent[] events) where TEvent : IEvent
        {
            foreach (var @event in events)
            {
                var eventType = @event.GetType();
                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
                await DispatchAsync(handlerType, @event);
            }
        }

        private async Task DispatchAsync<TEvent>(Type handlerType, TEvent @event) where TEvent : IEvent
        {
            if (_context.TryResolve(handlerType, out var handler))
            {
                var method = handler.GetType().GetMethod("HandleAsync");
                if (method != null) 
                    await (Task) method.Invoke(handler, new object[] {@event});
            }
        }
    }
}