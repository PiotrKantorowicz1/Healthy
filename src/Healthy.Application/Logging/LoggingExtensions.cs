using System;
using AutoMapper.Configuration;
using Healthy.Infrastructure.Extensions;
using Healthy.Infrastructure.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Healthy.Application.Logging
{
    public static class LoggingExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
         => webHostBuilder.UseSerilog((context, loggerConfiguration) =>
         {
             var serilogOptions = context.Configuration.GetOptions<SerilogSettings>("serilog");
             if (!Enum.TryParse<LogEventLevel>(serilogOptions.Level, true, out var level))
             {
                 level = LogEventLevel.Information;
             }
             Configure(loggerConfiguration, serilogOptions);
         });

        private static void Configure(LoggerConfiguration loggerConfiguration, SerilogSettings serilogOptions)
        {
            if (serilogOptions.ConsoleEnabled)
            {
                loggerConfiguration.WriteTo.Console();
            }
        }
    }
}