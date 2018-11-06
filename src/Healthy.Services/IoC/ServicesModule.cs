using System.Reflection;
using Autofac;
using Healthy.Contracts.Commands;
using Healthy.EventStore.EventsStore;
using Healthy.Infrastructure.Handlers;
using Healthy.Services.Services;

namespace Healthy.Services.IoC
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServicesModule)
                .GetTypeInfo()
                .Assembly;

            var contractsAssembly = typeof(ICommand)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterType<EventStore.EventsStore.EventStore>().As<IEventStore>();

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IService>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly, contractsAssembly)
                   .AsClosedTypesOf(typeof(ICommandHandler<>))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
