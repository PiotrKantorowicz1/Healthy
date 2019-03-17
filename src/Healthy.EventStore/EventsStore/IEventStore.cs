namespace Healthy.EventStore.EventsStore
{
    public interface IEventStore
    {
        //T Load<T>(Guid aggregateId, int? version = null) where T : AggregateRoot, new();
        //Task<IEnumerable<EventInfo>> Load(Guid aggregateId, int? version = null);
        //void Store<T>(T aggregate) where T : AggregateRoot;
    }
}