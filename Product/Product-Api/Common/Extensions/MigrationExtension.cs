using Microsoft.EntityFrameworkCore;
using ProductData.Persistence;

namespace ProductApi.Common.Extensions
{
    public static class MigrationExtension
    {
        public static void UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            dbContext.Database.Migrate();
        }
    }
}