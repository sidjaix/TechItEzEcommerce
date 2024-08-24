using Order_Api.Common.Extensions;
using Order_Api.Utility;
using Order_Data.Services.IServices;
using Order_Data.Services;
using MassTransit;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var environment = builder.Environment;
        // Retrieve the connection string of Azure App Config Store
        var useAzureAppConfig = builder.Configuration.GetValue<bool>("Azure:UseAzureAppConfig");
        if (useAzureAppConfig)
        {
            var azAppConfigConnectionString = builder.Configuration.GetValue<string>("Azure:AppConfig");
            builder.Configuration.AddAzureAppConfiguration(azAppConfigConnectionString);
        }
        var config = builder.Configuration;

        builder.AddController();

        builder.Services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                var host = environment.IsDevelopment()
                ? "amqp://guest:guest@localhost:5672"
                : builder.Configuration["RabbitMq:Host"];

                cfg.Host(host);
            });
        });

        // Add application Authentication configuration
        builder.AddAppAuthetication();

        // Add application Authorization configuration
        builder.Services.AddAuthorization();

        builder.AddCors();

        builder.AddDbContextPool();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<AuthenticationHandler>();

        builder.Services.AddHttpClient<IProductService, ProductService>(u =>
        {
            u.BaseAddress = environment.IsDevelopment() ?
            new Uri("http://localhost:5002") :
            new Uri(builder.Configuration["ServiceUrls:ProductApi"]);
        }).AddHttpMessageHandler<AuthenticationHandler>();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.AddSwaggerGen();

        builder.RegisterDependencyService();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors("default");

        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
            option.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
        });
        //}

        //app.UseHttpsRedirection();
        app.UseRouting();

        // Apply Authentication and Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Apply Pending Migration
        app.UseMigiration();

        app.Run();
    }
}
