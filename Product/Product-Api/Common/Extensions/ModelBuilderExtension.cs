
using Microsoft.EntityFrameworkCore;
using Product_Core;

namespace Product_Api.Common.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigiration(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplyMigration(serviceScope.ServiceProvider.GetService<ProductDbContext>());
    }
    public static void ApplyMigration(ProductDbContext dbContext)
    {
        if (dbContext != null)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                System.Console.WriteLine("Applying Migration from Product...");
                dbContext.Database.Migrate();
            }
        }
    }

}
