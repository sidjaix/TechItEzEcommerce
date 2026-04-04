using ApiCommon.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;

namespace ApiCommon.Extensions;

public static class LoggingExtensions
{
    public static IHostBuilder AddStandardSerilog(this IHostBuilder hostBuilder, string applicationName)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            Configure(loggerConfiguration, context.Configuration, applicationName);
        });

        return hostBuilder;
    }


    public static IApplicationBuilder UseCustomContextTracking(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserContextMiddleware>();
    }

    public static void Configure(LoggerConfiguration loggerConfiguration, IConfiguration configuration, string applicationName)
    {
        // Default to the internal Docker DNS name for Seq if not specified in appsettings

        var seqUrl = configuration["Seq:ServerUrl"] ?? "http://seq:80";

        loggerConfiguration
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApplicationID", applicationName)
        .Enrich.WithEnvironmentName()
        .Enrich.WithMachineName()
        .Enrich.With<ExceptionEnricher>()
        .WriteTo.Console()
        // Stream structured JSON over the network to Seq
        .WriteTo.Seq(seqUrl);
    }
}

public class ExceptionEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Exception == null)
        {
            return;
        }

        var exception = logEvent.Exception;
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Exception", exception.Message));
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("StackTrace", exception.StackTrace));
    }
}
