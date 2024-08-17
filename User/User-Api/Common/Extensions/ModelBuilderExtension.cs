using Microsoft.EntityFrameworkCore;
using User_Core;

namespace User_Api.Extensions;

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
            Console.WriteLine("Checking for pending migration(s) to applying...");
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying Migration...");
                dbContext.Database.Migrate();
                Console.WriteLine("Migration has applied");
            }
            else
            {
                Console.WriteLine("No pending migration(s) found to apply.");
            }
        }
    }

}
