namespace Healthy.Infrastructure.Host
{
    public interface IResolver
    {
        T Resolve<T>();
    }
}