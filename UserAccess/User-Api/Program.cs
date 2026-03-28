using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Logging.Extensions;
using UserAccess.API.Common.Middlewares;
using UserAccess.Infrastructure.Persistence.Repositories;
using UserAccess.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using UserAccess.Application.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using FluentValidation.AspNetCore;
using User_Api.Common.Filters;
using UserAccess.Core.Entities;
using UserAccess.Application.Interfaces;
using User.Application.Interfaces;
using User.Infrastructure.Persistence.Repositories;
using UserAccess.Infrastructure.Identity;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Retrieve the connection string of Azure App Config Store
        var useAzureAppConfig = builder.Configuration.GetValue<bool>("Azure:UseAzureAppConfig");
        if (useAzureAppConfig)
        {
            var azAppConfigConnectionString = builder.Configuration.GetValue<string>("Azure:AppConfig");
            builder.Configuration.AddAzureAppConfiguration(azAppConfigConnectionString);
        }
        var config = builder.Configuration;

        // For serilog logging
        // While working on migration, Comment out this line and then uncomment after migration is done, otherwise it will throw error
        builder.Host.AddLogging("User-Api");

        //Add services to the container.
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelAttribute>();
        }).AddNewtonsoftJson(o =>
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

        //Register Identity
        builder.Services
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<UserDbContext>()
        .AddDefaultTokenProviders();

        // Register db context pool for sql server
        builder.Services.AddDbContextPool<UserDbContext>((serviceProvider, options) =>
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            options
            .UseSqlServer(connectionString, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
                options.MigrationsAssembly(typeof(UserDbContext).Assembly.FullName);
            });
            //.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());  //should not be used in production, only for development purpose
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        // Register the Swagger generator, defining 1 or more Swagger documents
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, Array.Empty<string>()
                }
            });
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "User API",
                Version = "v1",
                Description = "An API to perform e-commerce User Authentication related operations",
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

        // Configure options
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

        var jwtSettings = builder.Configuration.GetSection("JWT");
        var secret = jwtSettings.GetValue<string>("Secret");
        var issuer = jwtSettings.GetValue<string>("Issuer");
        var audience = jwtSettings.GetValue<string>("Audience");
        var key = Encoding.ASCII.GetBytes(secret);

        // By default, the JWT handler maps certain claim types to Microsoft's proprietary ones.
        // This line prevents that mapping, ensuring that the original claim types from the token are preserved.
        // For example, 'sub' remains 'sub' and is not mapped to 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'.
        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                ValidateAudience = true
            };
        });
        builder.Services.AddAuthorization();

        // Register Dependency Services
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
        builder.Services.AddScoped<ResponseDto>();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ResponseDto).Assembly));
        builder.Services.AddFluentValidationAutoValidation();

        // Register Problem Details service and Exception Handler
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        // Add health check
        builder.Services.AddHealthChecks();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseCors("default");

        // Use Global Exception Handler
        app.UseExceptionHandler();

        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API V1");
            option.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
        });
        //}

        //app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        // For serilog logging
        app.UseCorrelationId();
        app.UseAuthorization();

        app.MapHealthChecks("/health");
        app.MapControllers();

        // Apply Pending Migration
        // app.UseMigiration();

        app.Run();
    }
}
