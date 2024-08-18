
using Microsoft.EntityFrameworkCore;
using Product_Core;

namespace Product_Api.Common.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigiration(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplyMigration(serviceScope.ServiceProvider.GetService<ProductDbContext>());
    }
    public static void ApplyMigration(ProductDbContext dbContext)
    {
        if (dbContext != null)
        {
            Console.WriteLine("Checking pending migration for Product-Api...");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying Migration for Product-Api...");
                dbContext.Database.Migrate();
            }
            else
            {
                Console.WriteLine("Did not find any pending migration for Product-Api...");
            }
        }
    }

}
