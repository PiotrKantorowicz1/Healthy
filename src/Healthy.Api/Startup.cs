using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Healthy.Api.Framework.Extensions;
using Healthy.Infrastructure.Settings;
using Healthy.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.IoC;
using Healthy.Application.IoC;

namespace Healthy.Api
{
    public class Startup
    {
        private static readonly string[] Headers = new[] { "x-total-count" };
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddRedis();
            services.AddJwt();
            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors =>
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .WithExposedHeaders(Headers));
            });

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

            builder.Populate(services);        
            builder.RegisterInstance(Configuration.GetSettings<MongoDbSettings>()).SingleInstance();
            builder.RegisterModule(new InfrastructureModule(Configuration));
            builder.RegisterModule<ApplicationModule>();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseAllForwardedHeaders();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseAccessTokenValidator();
            app.UseMvc();

            var databaseSettings = Container.Resolve<MongoDbSettings>();
            var databaseInitializer = Container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();
            if (databaseSettings.Seed)
            {
                var databaseSeeder = Container.Resolve<IDatabaseSeeder>();
                databaseSeeder.SeedAsync();
            }

            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
