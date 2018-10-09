using System.Reflection;
using Autofac;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Services;
using Healthy.Infrastructure.Extensions;
using Healthy.Infrastructure.Handlers;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Security;
using Healthy.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Healthy.Infrastructure.IoC
{
    public class InfrastructureModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public InfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(InfrastructureModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterInstance(_configuration.GetSettings<FacebookSettings>())
                   .SingleInstance();
                   
            builder.RegisterModule<MongoDbModule>();
            builder.RegisterType<Encrypter>().As<IEncrypter>().SingleInstance();

            builder.RegisterType<JwtHandler>().As<IJwtHandler>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<Handler>().As<IHandler>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IRepository>())
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}