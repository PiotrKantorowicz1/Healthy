using System;
using System.Threading.Tasks;

namespace Healthy.Infrastructure.Handlers
{
    public interface IHandler
    {
        IHandlerTask Run(Action run);
        IHandlerTask Run(Func<Task> runAsync);
        IHandlerTaskRunner Validate(Action validate);
        IHandlerTaskRunner Validate(Func<Task> validateAsync);
        void ExecuteAll();
        Task ExecuteAllAsync();
    }
}