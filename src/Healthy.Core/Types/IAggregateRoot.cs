namespace Healthy.Core.Types
{
    public interface IAggregateRoot : IEntity
    {
        AggregateId AggregateId { get; }
    }
}