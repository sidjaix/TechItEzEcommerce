using Microsoft.EntityFrameworkCore;
using User_Core;

namespace User_Api;

public static class ModelBuilderExtension
{
    public static void UseMigiration(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        ApplyMigration(serviceScope.ServiceProvider.GetService<UserDbContext>());
    }
    public static void ApplyMigration(UserDbContext dbContext)
    {
        if (dbContext != null)
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                System.Console.WriteLine("Applying Migration...");
                dbContext.Database.Migrate();
            }
        }
    }

}
