using Newtonsoft.Json;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Order_Core;
using Order_Data.Repository;
using Order_Api.Common.Extensions;
using Order_Data.Repository.IRepository;
using Order_Api.Utility;
using Order_Data.Services.IServices;
using Order_Data.Services;
using Order_Core.Models;

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

        //Add services to the container.
        builder.Services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        // Add application Authentication configuration
        builder.AddAppAuthetication();

        // Add application Authorization configuration
        builder.Services.AddAuthorization();

        // In production, modify this with the actual domains you want to allow
        builder.Services.AddCors(o => o.AddPolicy("default", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

        // Register db context pool for sql server
        builder.Services.AddDbContextPool<OrderDbContext>((serviceProvider, options) =>
        {
            var azureDB = config.GetConnectionString("OrderApi");
            options
            .UseSqlServer(azureDB)
            .EnableSensitiveDataLogging(environment.IsDevelopment());  //should not be used in production, only for development purpose
        });

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

        // Register the Swagger generator, defining 1 or more Swagger documents
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Description = $"Enter the Bearer Authorization string as following: `{JwtBearerDefaults.AuthenticationScheme} Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] {}
                }
            });
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Order API",
                Version = "v1",
                Description = "An API to perform e-commerce Order related operations",
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

        // Register Dependency Services
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<ResponseDto>();

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
