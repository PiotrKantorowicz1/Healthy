using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Healthy.Api.Framework;
using Healthy.Application.IoC;
using Healthy.Infrastructure.IoC;
using Healthy.Infrastructure.Mongo;
using Healthy.Infrastructure.Mongo.Settings;
using Healthy.Infrastructure.Security;
using Healthy.Infrastructure.Security.Settings;
using Healthy.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Healthy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            var options = Configuration.GetOptions<JwtTokenSettings>("jwt");
            services.AddSingleton(options);
            services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();
            services.AddAuthentication().AddJwtBearer();
        
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", cors =>
                      cors.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<InfrastructureModule>();
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

            var databaseSettings = app.ApplicationServices.GetService<MongoDbSettings>();
            var databaseInitializer = app.ApplicationServices.GetService<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();

            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();

            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
