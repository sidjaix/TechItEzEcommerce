using Order_Service.Utility.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var environment = builder.Environment;

        // Add services to the container.

        builder.AddController();

        builder.AddMassTransitRabbitMq();

        builder.AddCors();

        builder.AddDbContextPool();

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
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service/Consumer API V1");
            option.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
        });
        //  }
        app.UseRouting();

        app.MapControllers();

        app.Run();
    }
}