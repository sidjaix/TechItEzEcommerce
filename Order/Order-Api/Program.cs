using OrderData.Services;
using MassTransit;
using OrderApplication.Interfaces;
using FluentValidation.AspNetCore;
using OrderApplication.Queries;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ApiCommon.Extensions;
using OrderData.Persistence;
using Microsoft.EntityFrameworkCore;
using ApiCommon.Options;
using OrderData.Persistence.Repositories;
using ApiCommon.Handlers;
using OrderApi.Services;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var config = builder.Configuration;
		var environment = builder.Environment;
		var appName = "Order-Api";

		// ==========================================
		// 1. LOGGING & CONFIGURATION
		// ==========================================
		// Comment this line while working on EF Migrations to avoid issues with DB Context Configuration connection string not being available during design time
		builder.Host.AddStandardSerilog(appName);
		builder.Services.AddStandardOpenTelemetry(config, appName);

		var useAzureAppConfig = config.GetValue<bool>("Azure:UseAzureAppConfig");
		if (useAzureAppConfig)
		{
			var azAppConfigConnectionString = config.GetValue<string>("Azure:AppConfig");
			config.AddAzureAppConfiguration(azAppConfigConnectionString);
		}

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
		builder.Services.AddDbContextPool<OrderDbContext>((serviceProvider, options) =>
		{
			var connectionString = config.GetConnectionString("DefaultConnection");
			options.UseSqlServer(connectionString, sqlOptions =>
			{
				sqlOptions.EnableRetryOnFailure(
					maxRetryCount: 5,
					maxRetryDelay: TimeSpan.FromSeconds(30),
					errorNumbersToAdd: null
				);
				sqlOptions.MigrationsAssembly(typeof(OrderDbContext).Assembly.FullName);
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
		builder.Services.AddSwagger(appName);

		// ==========================================
		// 7. DEPENDENCY INJECTION & MISC SERVICES
		// ==========================================
		builder.Services.AddMassTransit(busConfig =>
		{
			busConfig.SetKebabCaseEndpointNameFormatter();
			busConfig.UsingRabbitMq((ctx, cfg) =>
			{
				var host = environment.IsDevelopment()
				? "amqp://guest:guest@localhost:5672"
				: config["RabbitMq:Host"];

				cfg.Host(host);
				cfg.ConfigureEndpoints(ctx);
			});
		});

		builder.Services.AddScoped<IOrderRepository, OrderRepository>();
		builder.Services.AddScoped<IOrderEventPublisher, OrderEventPublisher>();
		builder.Services.AddScoped<TokenDelegatingHandler>();

		builder.Services.AddHttpContextAccessor();
		builder.Services.AddHttpClient<ICartIntegrationService, CartIntegrationService>(u =>
		{
			// If running in VS natively, hit the exposed localhost port. 
			// If in Docker, use the internal Docker DNS name.
			u.BaseAddress = new Uri(config["Gateway:BaseUrl"] ?? "http://api_gateway:8080");

		}).AddHttpMessageHandler<TokenDelegatingHandler>();


		builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCustomerOrdersQuery).Assembly));
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
		app.UseSwaggerWUIWithAuth(appName);

		// 3. Routing (Figure out which endpoint is being called)
		app.UseRouting();

		// 4. CORS (Check if the caller is allowed to hit the routed endpoint)
		app.UseCors("default");

		// 5. Authentication & Logging (Identify the user)
		app.UseAuthentication();
		app.UseCustomContextTracking();

		// 6. Authorization (Check if the identified user has permissions)
		app.UseAuthorization();

		// 7. Map Endpoints (Execute the logic)
		app.MapHealthChecks("/health");
		app.MapControllers();

		app.Run();
	}
}
