using Autofac;
using Healthy.Core.Domain.Users.Repositories;
using Healthy.Core.Domain.Users.Services;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Repositories.Users;
using MongoDB.Driver;

namespace Healthy.Infrastructure.IoC
{
    public class DataModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<MongoDbModule>();
            builder.RegisterType<Encrypter>().As<IEncrypter>().SingleInstance();
            builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
            builder.RegisterType<OneTimeSecuredOperationRepository>().As<IOneTimeSecuredOperationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserSessionRepository>().As<IUserSessionRepository>().InstancePerLifetimeScope();
        }
    }
}