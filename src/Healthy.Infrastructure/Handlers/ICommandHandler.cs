using System.Threading.Tasks;
using Healthy.Core.Contracts.Commands;

namespace Healthy.Infrastructure.Handlers
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }

}