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
    public DbSet<ProductReview> ProductReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var cat = modelBuilder.Entity<Category>();
        cat
        .Property(b => b.CreatedOn)
        .HasDefaultValueSql("getdate()");
        cat
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        var prod = modelBuilder.Entity<Product>();
        prod
        .Property(b => b.CreatedOn)
        .HasDefaultValueSql("getdate()");
        prod
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");
        prod
        .HasOne<Category>()
        .WithMany()
        .HasForeignKey(a => a.CategoryId);

        var pr = modelBuilder.Entity<ProductReview>();
        pr
        .Property(b => b.CreatedOn)
        .HasDefaultValueSql("getdate()");
        pr
        .HasOne<Product>()
        .WithMany()
        .HasForeignKey(pr => pr.ProductId);

        base.OnModelCreating(modelBuilder);
    }
}
