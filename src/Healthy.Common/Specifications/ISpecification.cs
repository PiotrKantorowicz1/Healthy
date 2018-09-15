namespace Healthy.Common.Specifications
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T value);
    }
}