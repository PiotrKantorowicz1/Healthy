namespace Healthy.Core.Base
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T value);
    }
}