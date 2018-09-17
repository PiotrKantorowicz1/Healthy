using System;
using Healthy.Core.Extensions;
using Healthy.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Healthy.Infrastructure.Logging
{
    public static class LoggerExtensions
    {
        public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new SerilogSettings();
            configuration.GetSection("serilog").Bind(settings);
            services.AddSingleton<SerilogSettings>(settings);
        }

        public static void UseSerilog(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var settings = app.ApplicationServices.GetService<SerilogSettings>();
            if (settings.Level.Empty())
            {
                throw new ArgumentException("Log level can not be empty.", nameof(settings.Level));
            }
            var level = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), settings.Level, true);
            loggerFactory.AddSerilog();
            var configuration = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Is(level);

            if (settings.ConsoleEnabled)
            {
                configuration.WriteTo.Console(level).CreateLogger();
            }
        }
    }
}
