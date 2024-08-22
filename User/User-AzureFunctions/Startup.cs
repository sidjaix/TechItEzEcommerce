using Microsoft.Extensions.Configuration;
using User_Core;
using Microsoft.EntityFrameworkCore;
using User_Data.Repository.IRepository;
using User_Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace User_AzureFunctions;

public static class Startup
{
    public static void RegisterService(this IServiceCollection services)
    {
        // Set up configuration
        var config = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables() // This line adds environment variables to the configuration
            .Build();

        // Register DbContext with dependency injection
        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("AzureDB")));
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
