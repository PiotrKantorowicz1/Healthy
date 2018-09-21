using System.Reflection;
using Autofac;
using Healthy.Application.Mappers;
using Healthy.Application.Services;
using Healthy.Application.Dispatchers;
using Healthy.Infrastructure.Handlers;

namespace Healthy.Application.IoC
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ApplicationModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterInstance(AutoMapperConfig.InitializeMapper());

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IService>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                   .As<ICommandDispatcher>()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(ICommandHandler<>))
                   .InstancePerLifetimeScope();
        }
    }
}
