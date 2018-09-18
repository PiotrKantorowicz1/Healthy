using System;
using System.Threading.Tasks;
using Healthy.Core.Exceptions;
using Serilog;

namespace Healthy.Infrastructure.Handlers
{
    public interface IHandlerTask
    {
        IHandlerTask Always(Action always);
        IHandlerTask Always(Func<Task> always);
        IHandlerTask OnCustomError(Action<HealthyException> onCustomError, 
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Action<HealthyException, ILogger> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<HealthyException, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnCustomError(Func<HealthyException, ILogger, Task> onCustomError,
            bool propagateException = false, bool executeOnError = false);
        IHandlerTask OnError(Action<Exception> onError, bool propagateException = false);
        IHandlerTask OnError(Action<Exception, ILogger> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false);
        IHandlerTask OnError(Func<Exception, ILogger, Task> onError, bool propagateException = false);
        IHandlerTask OnSuccess(Action onSuccess);
        IHandlerTask OnSuccess(Func<Task> onSuccess);
        IHandlerTask PropagateException();
        IHandlerTask DoNotPropagateException();
        IHandler Next();
        void Execute();
        Task ExecuteAsync();
    }
}