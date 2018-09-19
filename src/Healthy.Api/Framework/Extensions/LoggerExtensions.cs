using System;
using Healthy.Infrastructure.Extensions;
using Healthy.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace Healthy.Api.Framework.Extensions
{
    public static class LoggerExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder, string applicationName = null)
            => webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var serilogOptions = context.Configuration.GetOptions<SerilogSettings>("serilog");
                if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
                {
                    level = LogEventLevel.Information;
                }

                loggerConfiguration.Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);
                Configure(loggerConfiguration, level, serilogOptions);
            });

        private static void Configure(LoggerConfiguration loggerConfiguration, LogEventLevel level,
           SerilogSettings serilogOptions)
        {
            if (serilogOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo.Console();
            }
        }
    }
}