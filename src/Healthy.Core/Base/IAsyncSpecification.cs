using System.Threading.Tasks;

namespace Healthy.Core.Base
{
    public interface IAsyncSpecification<in T>
    {
        Task<bool> IsSatisfiedByAsync(T value);
    }
}