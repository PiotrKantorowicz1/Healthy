using System.Reflection;
using Autofac;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Domain.Users.Services;
using Healthy.Infrastructure.Handlers;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Repositories.Users;
using Healthy.Infrastructure.Security;
using MongoDB.Driver;

namespace Healthy.Infrastructure.IoC
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(InfrastructureModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterModule<MongoDbModule>();
            builder.RegisterType<Encrypter>().As<IEncrypter>().SingleInstance();
            builder.RegisterType<Handler>().As<IHandler>();
            builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
            
            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IRepository>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope(); 
        }
    }
}