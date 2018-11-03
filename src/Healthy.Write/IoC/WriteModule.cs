using System.Reflection;
using Autofac;
using Healthy.Contracts.Commands;
using Healthy.Infrastructure.Handlers;
using Healthy.Write.Services;

namespace Healthy.Write.IoC
{
    public class WriteModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(WriteModule)
                .GetTypeInfo()
                .Assembly;

            var contractsAssembly = typeof(ICommand)
                .GetTypeInfo()
                .Assembly;

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
