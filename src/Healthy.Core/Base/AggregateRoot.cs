namespace Healthy.Core.Base
{
    public class AggregateRoot : Entity, IAggregateRoot
    {
        public AggregateId AggregateId { get; set; }
    }
}