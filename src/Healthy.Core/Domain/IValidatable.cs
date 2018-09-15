namespace Healthy.Core.Domain
{
    public interface IValidatable
    {
        bool IsValid { get; }
    }
}