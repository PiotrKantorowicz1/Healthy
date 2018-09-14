namespace Healthy.Core.Base
{
    public interface IAggregateRoot : IEntity
    {
        AggregateId AggregateId { get; }
    }
}