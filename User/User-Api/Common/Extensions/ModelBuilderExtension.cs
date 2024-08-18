using Microsoft.EntityFrameworkCore;
using User_Core;

namespace User_Api.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigiration(this IApplicationBuilder app)
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
