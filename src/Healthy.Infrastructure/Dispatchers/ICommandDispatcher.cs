using System.Threading.Tasks;
using Healthy.Contracts.Commands;

namespace Healthy.Infrastructure.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}