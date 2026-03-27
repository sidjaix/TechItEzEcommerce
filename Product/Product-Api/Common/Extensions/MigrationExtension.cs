using Microsoft.EntityFrameworkCore;
using Product_Core;

namespace Product_Api.Common.Extensions
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