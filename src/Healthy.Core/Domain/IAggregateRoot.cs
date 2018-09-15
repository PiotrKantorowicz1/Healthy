namespace Healthy.Core.Domain
{
    public interface IAggregateRoot : IEntity
    {
        AggregateId AggregateId { get; }
    }
}