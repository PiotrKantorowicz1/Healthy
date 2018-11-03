using System.Reflection;
using Autofac;
using Healthy.Infrastructure.Handlers;
using Healthy.Read.Mappers;
using Module = Autofac.Module;

namespace Healthy.Read.IoC
{
    public class ReadModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ReadModule)
                .GetTypeInfo()
                .Assembly;
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IMapper>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();                     
        }
    }
}