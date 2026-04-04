using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ApiCommon.Extensions;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddStandardOpenTelemetry(this IServiceCollection services, IConfiguration configuration, string applicationName)
    {
        // Internal Docker DNS for Jaeger
        var jaegerOtlpUrl = configuration["Jaeger:OtlpUrl"] ?? "http://jaeger:4317";

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(applicationName))
            .WithTracing(tracing =>
            {
                tracing
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        // Filter out noisy requests to keep Jaeger clean
                        options.Filter = context => !context.Request.Path.StartsWithSegments("/health") &&
                                                    !context.Request.Path.StartsWithSegments("/swagger");
                    })
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    // MassTransit v8 has built-in OTel support using this specific source name
                    .AddSource("MassTransit")
                    .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri(jaegerOtlpUrl);
                        options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                    });
            });

        return services;
    }
}
