using Order_Core;
using Microsoft.EntityFrameworkCore;

namespace Order_Api.Common.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigiration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplyMigration(serviceScope.ServiceProvider.GetService<OrderDbContext>());
    }
    public static void ApplyMigration(OrderDbContext dbContext)
    {
        if (dbContext != null)
        {
            Console.WriteLine("Checking pending migration for Order-Api...");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying Migration for Order-Api...");
                dbContext.Database.Migrate();
            }
            else
            {
                Console.WriteLine("Did not find any pending migration for Order-Api...");
            }
        }
    }
}
