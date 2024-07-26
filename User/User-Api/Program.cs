using User_Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using User_Data.Interface;
using User_Core;
using User_Api;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

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
        builder.Services.AddDbContextPool<UserDbContext>(options =>
        {
            options
            .LogTo(Console.WriteLine, LogLevel.Information)
            .UseSqlServer(configuration.GetConnectionString("DockerDBConnection"))
            .EnableSensitiveDataLogging();  //should not be used in production, only for development purpose
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register Dependency Services
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors("default");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseHttpsRedirection();
        app.UseRouting();
        app.MapControllers();

        // Apply Pending Migration
        ModelBuilderExtension.UseMigiration(app);

        app.Run();
    }
}
