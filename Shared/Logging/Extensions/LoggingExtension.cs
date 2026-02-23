using Logging.Enrichers;
using Logging.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Logging.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    public static WebApplicationBuilder AddSeyfarthLogging(this WebApplicationBuilder builder, string applicationName)
    {
        builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationId", applicationName)
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.With<ExceptionEnricher>()
                .WriteTo.Async(a => a.MSSqlServer(
                    connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "ErrorLog",
                        AutoCreateSqlTable = true
                    },
                    columnOptions: GetSqlColumnOptions()
                ));
        });

        return builder;
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
