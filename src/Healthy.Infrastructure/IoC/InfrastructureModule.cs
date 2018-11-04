using System.Reflection;
using Autofac;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Domain.Users.DomainClasses;
using Healthy.Core.Domain.Users.Services;
using Healthy.Core.Types;
using Healthy.Infrastructure.Dispatchers;
using Healthy.Infrastructure.Extensions;
using Healthy.Infrastructure.Files;
using Healthy.Infrastructure.Handlers;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Redis;
using Healthy.Infrastructure.Security;
using Healthy.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

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

            builder.Register((c, p) =>
                {
                    var settings = c.Resolve<MongoDbSettings>();
    
                    return new MongoClient(settings.ConnectionString);
                }).SingleInstance();                      

            builder.Register((c, p) =>
                {
                    var mongoClient = c.Resolve<MongoClient>();
                    var settings = c.Resolve<MongoDbSettings>();
                    var database = mongoClient.GetDatabase(settings.Database);
    
                    return database;
                }).As<IMongoDatabase>();

            builder.RegisterType<RedisDatabaseFactory>()
                .As<IRedisDatabaseFactory>()
                .SingleInstance();

            builder.Register((c, p) =>
                {
                    var settings = c.Resolve<RedisSettings>();
                    var databaseFactory = c.Resolve<IRedisDatabaseFactory>();
                    var database = databaseFactory.GetDatabase(settings.Database);

                    return database;
                }).As<Maybe<RedisDatabase>>()
                .SingleInstance();

            builder.RegisterType<RedisCache>()
                .As<ICache>()
                .SingleInstance();

            builder.RegisterInstance(_configuration.GetSettings<FacebookSettings>())
                .SingleInstance();
            
            builder.RegisterInstance(_configuration.GetSettings<RedisSettings>())
                .SingleInstance();

            builder.RegisterType<FileValidator>().As<IFileValidator>()
                .SingleInstance();

            builder.RegisterType<FileResolver>().As<IFileResolver>()
                .SingleInstance();

            builder.RegisterType<ImageService>().As<IImageService>()
                .SingleInstance();

            builder.RegisterType<Encrypter>().As<IEncrypter>()
                .SingleInstance();

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
                      
            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<Dispatcher>()
                .As<IDispatcher>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}