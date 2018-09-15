namespace Healthy.Core.Domain.BaseClasses
{
    public class AggregateRoot : Entity, IAggregateRoot
    {
        public AggregateId AggregateId { get; set; }
    }
}