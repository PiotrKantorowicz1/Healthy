using System.Reflection;
using Autofac;
using Healthy.Contracts.Events;
using Healthy.EventStore.Caching;
using Healthy.Infrastructure.Handlers;
using Module = Autofac.Module;

namespace Healthy.EventStore.IoC
{
    public class EventStoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(EventStoreModule)
                .GetTypeInfo()
                .Assembly;

            var contractsAssembly = typeof(IEvent)
                .GetTypeInfo()
                .Assembly;
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<ICacheMarker>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(assembly, contractsAssembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}