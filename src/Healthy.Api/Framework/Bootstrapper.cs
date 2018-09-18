using System.Reflection;
using Autofac;
using Healthy.Api.NancyExtensions;
using Healthy.Application.IoC;
using Healthy.Infrastructure.Extensions;
using Healthy.Infrastructure.Handlers;
using Healthy.Infrastructure.IoC;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Security;
using Healthy.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace Healthy.Api.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly ILogger Logger = Log.Logger;
        private readonly IConfiguration _configuration;
        public static ILifetimeScope LifetimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

#if DEBUG
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
#endif

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            container.Update(builder =>
            {
                var assembly = typeof(Startup).GetTypeInfo().Assembly;

                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<MongoDbSettings>());
                builder.RegisterInstance(_configuration.GetSettings<FaceBookSettings>());
                builder.RegisterInstance(_configuration.GetSettings<JwtTokenSettings>());

                builder.RegisterModule<InfrastructureModule>();
                builder.RegisterModule<ApplicationModule>();
              
                builder.RegisterAssemblyTypes(assembly)
                       .AsClosedTypesOf(typeof(ICommandHandler<>))
                       .InstancePerLifetimeScope();

                SecurityContainer.Register(builder, _configuration);

            });
            LifetimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.SetupTokenAuthentication(container.Resolve<IJwtTokenHandler>());
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var databaseSettings = container.Resolve<MongoDbSettings>();
            var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            Logger.Information("Healthy API has started.");
        }
    }
}