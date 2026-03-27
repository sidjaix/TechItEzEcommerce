using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Order_Api.Utility;
using Order_Core;
using Order_Data.Repository;
using Order_Data.Repository.IRepository;
using Order_Data.Services;
using Order_Data.Services.IServices;

namespace Order_Api.Common.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        // Register controllers with NewtonsoftJson
        services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        // Register DbContext
        services.AddDbContextPool<OrderDbContext>(options =>
        {
            var connectionString = config.GetConnectionString("OrderApi");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            })
            .EnableSensitiveDataLogging(environment.IsDevelopment());
        });

        // Register HttpContextAccessor and services
        services.AddHttpContextAccessor();
        services.AddScoped<AuthenticationHandler>();
        services.AddHttpClient<IProductService, ProductService>(u =>
        {
            u.BaseAddress = environment.IsDevelopment()
                ? new Uri("http://localhost:5002")
                : new Uri(config["ServiceUrls:ProductApi"]);
        }).AddHttpMessageHandler<AuthenticationHandler>();

        // Register repositories
        services.AddScoped<IOrderRepository, OrderRepository>();

        // MassTransit
        services.AddMassTransit(busConfig =>
        {
            busConfig.SetKebabCaseEndpointNameFormatter();
            busConfig.UsingRabbitMq((ctx, cfg) =>
            {
                var host = environment.IsDevelopment()
                ? "amqp://guest:guest@localhost:5672"
                : config["RabbitMq:Host"];
                cfg.Host(host);
            });
        });


        return services;
    }
}