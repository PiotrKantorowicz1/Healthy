namespace Healthy.Core.Types
{
    public class AggregateRoot : Entity, IAggregateRoot
    {
        public AggregateId AggregateId { get; set; }
    }
}