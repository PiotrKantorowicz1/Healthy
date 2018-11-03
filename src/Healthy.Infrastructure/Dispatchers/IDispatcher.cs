using System;
using System.Linq;
using System.Threading.Tasks;
using Healthy.Contracts.Commands;
using Healthy.Contracts.Events;
using Healthy.Core.Pagination;

namespace Healthy.Infrastructure.Dispatchers
{
    public interface IDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
        Task DispatchAsync<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}