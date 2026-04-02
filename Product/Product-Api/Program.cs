using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using ProductApi.Common.Extensions;
using ProductApplication.Interfaces;
using ProductData.Repositories;
using ProductData.Persistence;
using Logging.Extensions;
using ProductApi.Common.Filters;
using ProductApi.Common.Options;
using ProductApplication.DTOs;
using FluentValidation.AspNetCore;
using Logging.Middlewares;
using ApiCommon.Extensions;
using ProductApplication.Queries;
using MassTransit;
using ProductApi.Consumers;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;
        var environment = builder.Environment;

        // ==========================================
        // 1. CONFIGURATION
        // ==========================================
        var useAzureAppConfig = config.GetValue<bool>("Azure:UseAzureAppConfig");
        if (useAzureAppConfig)
        {
            var azAppConfigConnectionString = config.GetValue<string>("Azure:AppConfig");
            config.AddAzureAppConfiguration(azAppConfigConnectionString);
        }

        // Comment this line while working on EF Migrations to avoid issues with DB Context Configuration connection string not being available during design time
        builder.Host.AddLogging("Product-Api");

        // ==========================================
        // 2. CONTROLLERS & JSON FORMATTING
        // ==========================================
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelAttribute>();
        }).AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        // ==========================================
        // 3. CORS POLICY: In production, modify this with the actual domains you want to allow
        // ==========================================
        builder.Services.AddCorsPolicy();

        // ==========================================
        // 4. DATABASE & IDENTITY (Always Registered)
        // ==========================================
        builder.Services.AddDbContextPool<ProductDbContext>((serviceProvider, options) =>
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                sqlOptions.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName);
            });

            // Keep sensitive data logging restricted to development
            if (environment.IsDevelopment())
            {
                //options.EnableSensitiveDataLogging();
            }
        });

        // ==========================================
        // 5. AUTHENTICATION & AUTHORIZATION
        // ==========================================
        builder.Services.Configure<JwtOptions>(config.GetSection("JWT"));
        builder.AddAppAuthentication();

        // ==========================================
        // 6. SWAGGER / OPENAPI
        // ==========================================
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger("Product API");

        // ==========================================
        // 7. DEPENDENCY INJECTION & MISC SERVICES
        // ==========================================
        builder.Services.AddMassTransit(busConfig =>
        {
            // This will create a queue named: product-order-placed-event
            busConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("product", false));

            // THIS LINE IS REQUIRED TO BIND THE QUEUE
            busConfig.AddConsumer<OrderPlacedEventConsumer>();

            busConfig.UsingRabbitMq((ctx, cfg) =>
            {
                var host = environment.IsDevelopment()
                ? "amqp://guest:guest@localhost:5672"
                : builder.Configuration["RabbitMq:Host"];

                cfg.Host(host);
                cfg.ConfigureEndpoints(ctx);
            });
        });

        // In Program.cs or DependencyInjection.cs
        builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
        builder.Services.AddScoped<ITaxonomyRepository, TaxonomyRepository>();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetActiveCatalogItemsQuery).Assembly));
        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddHealthChecks();

        var app = builder.Build();

        // ==========================================
        // 8. HTTP REQUEST PIPELINE (Strict Ordering)
        // ==========================================

        // 1. Error Handling (Catch errors early)
        app.UseExceptionHandler();

        // 2. Swagger (Serve documentation)
        app.UseSwaggerWUIWithAuth();

        // 3. Routing (Figure out which endpoint is being called)
        app.UseRouting();

        // 4. CORS (Check if the caller is allowed to hit the routed endpoint)
        app.UseCors("default");

        // 5. Authentication & Logging (Identify the user and start correlation)
        app.UseAuthentication();
        app.UseCorrelationId();

        // 6. Authorization (Check if the identified user has permissions)
        app.UseAuthorization();

        // 7. Map Endpoints (Execute the logic)
        app.MapHealthChecks("/health");
        app.MapControllers();

        // Ensure the database is initialized and seeded before handling requests
        await app.InitializeDatabaseAsync();

        app.Run();
    }
}
