using System;
using System.Threading.Tasks;

namespace Healthy.Infrastructure.Handlers
{
    public interface IHandlerTaskRunner
    {
        IHandlerTask Run(Action run);
        IHandlerTask Run(Func<Task> runAsync);         
    }
}