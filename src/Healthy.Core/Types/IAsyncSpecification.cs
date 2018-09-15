using System.Threading.Tasks;

namespace Healthy.Core.Types
{
    public interface IAsyncSpecification<in T>
    {
        Task<bool> IsSatisfiedByAsync(T value);
    }
}