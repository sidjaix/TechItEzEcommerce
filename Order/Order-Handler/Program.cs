using MassTransit;
using Order_Core.Models;
using Order_Data.Repository;
using Order_Data.Repository.IRepository;
using OrderService;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var environment = builder.Environment;

        builder.Services.AddControllers();

        builder.Services.AddMassTransit(config =>
        {
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderConsumer>();

                config.UsingRabbitMq((ctx, cfg) =>
                {
                    var host = environment.IsDevelopment() ? "amqp://guest:guest@localhost:5672" : builder.Configuration.GetValue<string>("RabbitMq:Host");
                    cfg.Host(host);
                    cfg.ReceiveEndpoint("order-queue", c =>
                    {
                        c.ConfigureConsumer<OrderConsumer>(ctx);
                    });
                });
            });
        });

        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.Run();
    }
}