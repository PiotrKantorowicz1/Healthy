using System.Reflection;
using Autofac;
using Healthy.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Healthy.Infrastructure.Mongo
{
    public class MongoDbModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
        }
    }
}