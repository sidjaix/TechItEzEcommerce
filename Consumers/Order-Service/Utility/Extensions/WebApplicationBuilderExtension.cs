using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Newtonsoft.Json;
using MassTransit;

using OrderService.EventHandlers;
using OrderData.Persistence;
using App_Contracts.Common;

namespace OrderService.Utility.Extensions;

public static class Common_Extensions_WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddSwaggerGen(this WebApplicationBuilder builder)
    {
        // Register the Swagger generator, defining 1 or more Swagger documents
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Order Handler API",
                Version = "v1",
                Description = "An API to perform e-commerce Order Service/Consumer related operations",
                TermsOfService = new Uri("https://twitter.com/sidjaix"),
                Contact = new OpenApiContact
                {
                    Name = "Siddharth Jaiswal",
                    Email = "sidjaix@tie.com",
                    Url = new Uri("https://twitter.com/sidjaix"),
                },
                License = new OpenApiLicense
                {
                    Name = "Tech It Ez e-Commerce API LICX",
                    Url = new Uri("https://twitter.com/sidjaix"),
                }
            });
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
        return builder;
    }

    public static WebApplicationBuilder AddDbContextPool(this WebApplicationBuilder builder)
    {
        var dbConnectionString = builder.Configuration.GetConnectionString("OrderDb");
        // Register db context pool for sql server
        builder.Services.AddDbContextPool<OrderDbContext>((serviceProvider, options) =>
        {
            options
            .UseSqlServer(dbConnectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            })
            .EnableSensitiveDataLogging(builder.Environment.IsDevelopment());  //should not be used in production, only for development purpose
        });
        return builder;
    }

    public static WebApplicationBuilder RegisterDependencyService(this WebApplicationBuilder builder)
    {
        // Register Dependency Services
        builder.Services.AddScoped<ResponseDto>();

        return builder;
    }

    public static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        // In production, modify this with the actual domains you want to allow
        builder.Services.AddCors(o => o.AddPolicy("default", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
        return builder;
    }

    public static WebApplicationBuilder AddController(this WebApplicationBuilder builder)
    {
        //Add services to the container.
        builder.Services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });
        return builder;
    }

    public static WebApplicationBuilder AddMassTransitRabbitMq(this WebApplicationBuilder builder)
    {
        builder.Services.AddMassTransit(config =>
       {
           builder.Services.AddMassTransit(config =>
           {
               config.AddConsumer<OrderCreateHandler>();
               config.SetKebabCaseEndpointNameFormatter();
               config.UsingRabbitMq((ctx, cfg) =>
               {
                   Console.WriteLine("IsDevelopment: {0}", builder.Environment.IsDevelopment());
                   var queueName = builder.Configuration["RabbitMq:OrderQueueName"] ?? string.Empty;
                   var host = builder.Environment.IsDevelopment() ? "amqp://guest:guest@localhost:5672" : builder.Configuration.GetValue<string>("RabbitMq:Host");
                   cfg.Host(host);
                   cfg.ReceiveEndpoint(queueName, c =>
                   {
                       c.ConfigureConsumer<OrderCreateHandler>(ctx);
                   });
               });
           });
       });
        return builder;
    }
}
