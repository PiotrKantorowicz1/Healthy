using System.Threading.Tasks;

namespace Healthy.Common.Specifications
{
    public interface IAsyncSpecification<in T>
    {
        Task<bool> IsSatisfiedByAsync(T value);
    }
}