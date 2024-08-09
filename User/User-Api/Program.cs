using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using User_Core;
using User_Data;
using User_Api.Extensions;
using User_Data.Interface;
using Microsoft.OpenApi.Models;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Retrieve the connection string of Azure App Config Store
        string connectionString = builder.Configuration.GetConnectionString("AppConfig");
        builder.Configuration.AddAzureAppConfiguration(connectionString);
        var config = builder.Configuration;

        //Add services to the container.
        builder.Services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });
        // In production, modify this with the actual domains you want to allow
        builder.Services.AddCors(o => o.AddPolicy("default", builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        }));
        // Register db context pool for sql server
        builder.Services.AddDbContextPool<UserDbContext>((serviceProvider, options) =>
        {
            var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var azureDB = config.GetConnectionString("AzureDB");
            options
            .UseSqlServer(azureDB)
            .EnableSensitiveDataLogging(environment.IsDevelopment());  //should not be used in production, only for development purpose
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        // Register the Swagger generator, defining 1 or more Swagger documents
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "User API",
                Version = "v1",
                Description = "An API to perform e-commerce User related operations",
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
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<IAdminRepository, AdminRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors("default");
        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
            option.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
        });
        //}
        //app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();

        // Apply Pending Migration
        ModelBuilderExtension.UseMigiration(app);

        app.Run();
    }
}
