using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;

using CartData.Persistence;
using CartApplication.Interfaces;
using CartData.Persistence.Repositories;
using CartApplication.DTOs;
using CartApi.Utility;
using Logging.Extensions;
using ApiCommon.Extensions;
using Logging.Middlewares;
using FluentValidation.AspNetCore;
using ApiCommon.Options;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var config = builder.Configuration;

        // ==========================================
        // 1. CONFIGURATION & LOGGING
        // ==========================================
        var useAzureAppConfig = config.GetValue<bool>("Azure:UseAzureAppConfig");
        if (useAzureAppConfig)
        {
            var azAppConfigConnectionString = config.GetValue<string>("Azure:AppConfig");
            config.AddAzureAppConfiguration(azAppConfigConnectionString);
        }

        // Comment this line while working on EF Migrations to avoid issues with DB Context Configuration connection string not being available during design time
        builder.Host.AddLogging("Cart-Api");

        // ==========================================
        // 2. CONTROLLERS & JSON FORMATTING
        // ==========================================
        builder.Services.AddControllers(options =>
        {
            //options.Filters.Add<ValidateModelAttribute>();
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
        builder.Services.AddDbContextPool<CartDbContext>((serviceProvider, options) =>
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                sqlOptions.MigrationsAssembly(typeof(CartDbContext).Assembly.FullName);
            });

            // Keep sensitive data logging restricted to development
            if (builder.Environment.IsDevelopment())
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
        builder.Services.AddSwagger("Cart API");

        // ==========================================
        // 7. DEPENDENCY INJECTION & MISC SERVICES
        // ==========================================
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<ResponseDto>();

        builder.Services.AddHttpContextAccessor();
        /*
        builder.Services.AddScoped<AuthenticationDelegateHandler>();
        builder.Services.AddHttpClient<ICartIntegrationService, ProductService>(u =>
        {
            u.BaseAddress = new Uri(config["ServiceUrls:ApiGateway"]);
        }).AddHttpMessageHandler<AuthenticationDelegateHandler>();
        */

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ResponseDto).Assembly));
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
        app.UseSwaggerWUIWithAuth("Cart API");

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
        //await app.InitializeDatabaseAsync();

        app.Run();
    }
}
