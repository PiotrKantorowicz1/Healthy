using System;
using System.Linq;
using System.Threading.Tasks;
using Healthy.Contracts.Commands;
using Healthy.Contracts.Events;
using Healthy.Core.Pagination;

namespace Healthy.Infrastructure.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly IEventDispatcher _eventDispatcher;

        public Dispatcher(ICommandDispatcher commandDispatcher, 
            IQueryDispatcher queryDispatcher, 
            IEventDispatcher eventDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _eventDispatcher = eventDispatcher;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
            => await _commandDispatcher.DispatchAsync(command);

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => await _queryDispatcher.QueryAsync(query);

        public async Task DispatchAsync<TEvent>(params TEvent[] events) where TEvent : IEvent
            => await _eventDispatcher.DispatchAsync(events);
    }
}