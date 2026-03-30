using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ApiCommon.Extensions;

public static class SwaggerExtension
{
    /// <summary>
    /// This method sets up Swagger for API documentation and testing. 
    /// It configures the security definition for JWT Bearer authentication,
    /// </summary>
    /// <param name="services"></param>
    /// <param name="apiTitle"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, string apiTitle)
    {
        services.AddSwaggerGen(c =>
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
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, Array.Empty<string>()
                }
            });
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = apiTitle,
                Version = "v1",
                Description = $"An API to perform e-commerce {apiTitle} related operations",
                Contact = new OpenApiContact { Name = "Siddharth Jaiswal", Email = "sidjaix@tie.com" }
            });

            var baseDirectory = AppContext.BaseDirectory;

            // Find all XML files in the output directory
            var xmlFiles = Directory.GetFiles(baseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var xmlFile in xmlFiles)
            {
                // Swagger will gracefully merge the documentation from all found files
                c.IncludeXmlComments(xmlFile);
            }

            // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath);
        });
        return services;
    }

    /// <summary>
    /// This method configures the application to use Swagger middleware and sets up the Swagger UI at the root URL.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseSwaggerWUIWithAuth(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(option =>
        {
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
            option.RoutePrefix = string.Empty;
        });
        return app;
    }
}