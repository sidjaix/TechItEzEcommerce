using Microsoft.EntityFrameworkCore;
using Product_Core.Entities;

namespace Product_Core;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
          : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<Variation> Variations { get; set; }
    public DbSet<VariationOption> VariationOptions { get; set; }
    public DbSet<ProductConfiguration> ProductConfigurations { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionCategory> PromotionCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
