using OrderData.Persistence;

namespace OrderApi.Common.Extensions;

public static class ModelBuilderExtension
{
    public static void UseMigiration(this IApplicationBuilder app)
    {

    }
    public static void ApplyMigration(OrderDbContext dbContext)
    {
    }
}
