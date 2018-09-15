namespace Healthy.Core.Domain.BaseClasses
{
    public interface IAggregateRoot : IEntity
    {
        AggregateId AggregateId { get; }
    }
}