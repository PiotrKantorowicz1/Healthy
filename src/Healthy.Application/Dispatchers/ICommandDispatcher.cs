using System.Threading.Tasks;
using Healthy.Core.Contracts.Commands;

namespace Healthy.Application.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}