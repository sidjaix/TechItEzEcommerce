using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using User_Api.Common.Filters;
using UserAccess.Core.Entities;
using Entity = UserAccess.Core.Entities;
using UserAccess.Application.Interfaces;
using UserAccess.API.Common.Middlewares;
using UserAccess.Application.Dtos;
using UserAccess.Infrastructure.Persistence;
using UserAccess.Infrastructure.Persistence.Repositories;

namespace User_Api.Common.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        // Add services to the container.
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelAttribute>();
        }).AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        //Register Identity
        services.AddIdentity<Entity.User, Role>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();

        // Register db context pool for sql server
        services.AddDbContextPool<UserDbContext>(options =>
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                sqlOptions.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName);
            });
        });

        // Register Dependency Services
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ResponseDto>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ResponseDto).Assembly));
        services.AddFluentValidationAutoValidation();

        // Register Problem Details service and Exception Handler
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        // Add health check
        services.AddHealthChecks();

        return services;
    }
}