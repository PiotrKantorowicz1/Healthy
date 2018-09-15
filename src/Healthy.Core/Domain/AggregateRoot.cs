namespace Healthy.Core.Domain
{
    public class AggregateRoot : Entity, IAggregateRoot
    {
        public AggregateId AggregateId { get; set; }
    }
}