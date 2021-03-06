using System;
using System.Threading.Tasks;
using Healthy.Core.Exceptions;
using Serilog;

namespace Healthy.Infrastructure.Handlers
{
    public class HandlerTask : IHandlerTask
    {
        private static readonly ILogger Logger = Log.Logger;
        private readonly IHandler _handler;
        private readonly Action _run;
        private readonly Func<Task> _runAsync;
        private Action _validate;
        private Func<Task> _validateAsync;
        private Action _always;
        private Func<Task> _alwaysAsync;
        private Action _onSuccess;
        private Func<Task> _onSuccessAsync;
        private Action<Exception> _onError;
        private Action<Exception, ILogger> _onErrorWithLogger;
        private Action<HealthyException> _onCustomError;
        private Action<HealthyException, ILogger> _onCustomErrorWithLogger;
        private Func<Exception, Task> _onErrorAsync;
        private Func<Exception, ILogger, Task> _onErrorWithLoggerAsync;
        private Func<HealthyException, Task> _onCustomErrorAsync;
        private Func<HealthyException, ILogger, Task> _onCustomErrorWithLoggerAsync;
        private bool _propagateException = true;
        private bool _executeOnError = true;

        public HandlerTask(IHandler handler, Action run)
        {
            _handler = handler;
            _run = run;
        }

        public HandlerTask(IHandler handler, Func<Task> runAsync)
        {
            _handler = handler;
            _runAsync = runAsync;
        }

        public HandlerTask(IHandler handler, Action run, 
            Action validate = null, Func<Task> validateAsync = null)
        {
            _handler = handler;
            _run = run;
            _validate = validate;
            _validateAsync = validateAsync;
        }

        public HandlerTask(IHandler handler, Func<Task> runAsync, 
            Action validate = null, Func<Task> validateAsync = null)
        {
            _handler = handler;
            _runAsync = runAsync;
            _validate = validate;
            _validateAsync = validateAsync;
        }

        public IHandlerTask Always(Action always)
        {
            _always = always;

            return this;
        }

        public IHandlerTask Always(Func<Task> always)
        {
            _alwaysAsync = always;

            return this;
        }

        public IHandlerTask OnCustomError(Action<HealthyException> onCustomError, 
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomError = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Action<HealthyException, ILogger> onCustomError, 
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLogger = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<HealthyException, Task> onCustomError, 
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnCustomError(Func<HealthyException, ILogger, Task> onCustomError, 
            bool propagateException = false, bool executeOnError = false)
        {
            _onCustomErrorWithLoggerAsync = onCustomError;
            _propagateException = propagateException;
            _executeOnError = executeOnError;

            return this;
        }

        public IHandlerTask OnError(Action<Exception> onError, bool propagateException = false)
        {
            _onError = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Action<Exception, ILogger> onError, bool propagateException = false)
        {
            _onErrorWithLogger = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, Task> onError, bool propagateException = false)
        {
            _onErrorAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnError(Func<Exception, ILogger, Task> onError, bool propagateException = false)
        {
            _onErrorWithLoggerAsync = onError;
            _propagateException = propagateException;

            return this;
        }

        public IHandlerTask OnSuccess(Action onSuccess)
        {
            _onSuccess = onSuccess;

            return this;
        }

        public IHandlerTask OnSuccess(Func<Task> onSuccess)
        {
            _onSuccessAsync = onSuccess;

            return this;
        }

        public IHandlerTask PropagateException()
        {
            _propagateException = true;

            return this;
        }

        public IHandlerTask DoNotPropagateException()
        {
            _propagateException = false;

            return this;
        }

        public IHandler Next() => _handler;

        public void Execute()
        {
            try
            {
                _validate?.Invoke();
                _run();
                _onSuccess?.Invoke();
            }
            catch (Exception exception)
            {
                var customException = exception as HealthyException;
                if (customException != null)
                {
                    _onCustomErrorWithLogger?.Invoke(customException, Logger);
                    _onCustomError?.Invoke(customException);
                }
                
                var executeOnError = _executeOnError || customException == null;
                if(executeOnError)
                {
                    _onErrorWithLogger?.Invoke(customException, Logger);
                    _onError?.Invoke(exception);
                }          
                if(_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                _always?.Invoke();
            }
        }

        public async Task ExecuteAsync()
        {
            try
            {
                _validate?.Invoke();
                if(_validateAsync != null)
                {
                    await _validateAsync();
                }
                await _runAsync();
                if(_onSuccessAsync != null)
                {
                    await _onSuccessAsync();
                }
            }
            catch (Exception exception)
            {
                var customException = exception as HealthyException;
                if (customException != null)
                {
                    _onCustomErrorWithLogger?.Invoke(customException, Logger);
                    if (_onCustomErrorWithLoggerAsync != null)
                    {
                        await _onCustomErrorWithLoggerAsync(customException, Logger);
                    }
                    _onCustomError?.Invoke(customException);
                    if (_onCustomErrorAsync != null)
                    {
                        await _onCustomErrorAsync(customException);
                    }
                }
                
                var executeOnError = _executeOnError || customException == null;
                if(executeOnError)
                {
                    _onErrorWithLogger?.Invoke(customException, Logger);
                    if (_onErrorWithLoggerAsync != null)
                    {
                        await _onErrorWithLoggerAsync(exception, Logger);
                    }
                    _onError?.Invoke(exception);
                    if (_onErrorAsync != null)
                    {
                        await _onErrorAsync(exception);
                    }
                }              
                if(_propagateException)
                {
                    throw;
                }
            }
            finally
            {
                if (_alwaysAsync != null)
                {
                    await _alwaysAsync();
                }
            }
        }
    }
}