using Microsoft.EntityFrameworkCore;
using UserAccess.Infrastructure.Persistence;

namespace UserAccess.API.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplyMigration(serviceScope.ServiceProvider.GetService<UserDbContext>());
    }
    public static void ApplyMigration(UserDbContext dbContext)
    {
        if (dbContext != null)
        {
            Console.WriteLine("Checking pending migration from User-Api...");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying Migration for User-Api...");
                dbContext.Database.Migrate();
            }
            else
            {
                Console.WriteLine("Did not find any pending migration for User-Api...");
            }
        }
    }

}
