using Logging.Enrichers;
using Logging.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Diagnostics.CodeAnalysis;

namespace Logging.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    public static IHostBuilder AddLogging(this IHostBuilder hostBuilder, string applicationName)
    {
        hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
        {
            Configure(loggerConfiguration, context.Configuration, applicationName);
        });

        return hostBuilder;
    }

    public static void Configure(LoggerConfiguration loggerConfiguration, IConfiguration configuration, string applicationName)
    {
        loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationID", applicationName)
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.With<ExceptionEnricher>()
                .WriteTo.Async(a => a.MSSqlServer(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "ErrorLog",
                        AutoCreateSqlTable = true
                    },
                    columnOptions: GetSqlColumnOptions()
                ));
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }

    private static ColumnOptions GetSqlColumnOptions()
    {
        var options = new ColumnOptions();
        options.Store.Remove(StandardColumn.Properties);
        options.Store.Add(StandardColumn.LogEvent);
        options.AdditionalColumns = [
            new SqlColumn("ApplicationID", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("CorrelationId", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("RequestId", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("RequestPath", System.Data.SqlDbType.NVarChar, dataLength: 255),
            new SqlColumn("ActionName", System.Data.SqlDbType.NVarChar, dataLength: 255),
            new SqlColumn("UserTKID", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("StackTrace", System.Data.SqlDbType.NVarChar, dataLength: -1),
            new SqlColumn("EnvironmentName", System.Data.SqlDbType.NVarChar, dataLength: 100),
            new SqlColumn("MachineName", System.Data.SqlDbType.NVarChar, dataLength: 100),
        ];
        return options;
    }
}
