using Cart_Api.Utility;
using Cart_Core;
using Cart_Data.Repository;
using Cart_Data.Repository.IRepository;
using Cart_Data.Services;
using Cart_Data.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cart_Api.Common.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment environment)
    {
        // Register controllers with NewtonsoftJson
        services.AddControllers().AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = Formatting.Indented;
            o.SerializerSettings.ContractResolver = new DefaultContractResolver();
        });

        // Register DbContext
        services.AddDbContextPool<CartDbContext>(options =>
        {
            var connectionString = config.GetConnectionString("CartApi");
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );
            })
            .EnableSensitiveDataLogging(environment.IsDevelopment());
        });

        // Register HttpContextAccessor and services
        services.AddHttpContextAccessor();
        services.AddScoped<AuthenticationDelegateHandler>();
        services.AddHttpClient<IProductService, ProductService>(u =>
        {
            u.BaseAddress = environment.IsDevelopment()
                ? new Uri("http://localhost:5002")
                : new Uri(config["ServiceUrls:ProductApi"]);
        }).AddHttpMessageHandler<AuthenticationDelegateHandler>();

        // Register repositories
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IWishlistRepository, WishlistRepository>();
        services.AddScoped<ICheckoutRepository, CheckoutRepository>();

        return services;
    }
}