using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserAccess.AzureFunctions;
using Microsoft.Extensions.Configuration;
using Logging.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
        .AddLogging("User-AzureFunctions")
        .ConfigureFunctionsWebApplication()
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            services.AddApplicationInsightsTelemetryWorkerService();
            services.ConfigureFunctionsApplicationInsights();
            services.RegisterService(context.Configuration);
        })
        .Build();

        host.Run();
    }
}