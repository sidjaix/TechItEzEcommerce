using Cart_Core;
using Microsoft.EntityFrameworkCore;

namespace Cart_Api.Common.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigiration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplyMigration(serviceScope.ServiceProvider.GetService<CartDbContext>());
    }
    public static void ApplyMigration(CartDbContext dbContext)
    {
        if (dbContext != null)
        {
            Console.WriteLine("Checking pending migration for Cart-Api...");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying Migration for Cart-Api...");
                dbContext.Database.Migrate();
            }
            else
            {
                Console.WriteLine("Did not find any pending migration for Cart-Api...");
            }
        }
    }
}
