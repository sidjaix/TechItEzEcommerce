using Microsoft.Extensions.DependencyInjection;

namespace ApiCommon.Extensions;

public static class CorsExtension
{
    /// <summary>
    /// In production, modify this with the actual domains you want to allow instead of AllowAnyOrigin for better security. 
    /// You can also create multiple policies for different environments or use cases.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy("default", builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        return services;
    }
}