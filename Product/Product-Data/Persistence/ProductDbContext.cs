using Microsoft.EntityFrameworkCore;
using ProductCore.Entities;

namespace ProductData.Persistence;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionCategory> PromotionCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity
            .Property(e => e.CategoryDescription)
            .HasMaxLength(255);

            entity
            .Property(e => e.CategoryImageUrl)
            .HasMaxLength(255);

            entity
            .Property(e => e.CategoryName)
            .IsRequired()
            .HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryId");

            entity.Property(e => e.ProductName)
            .IsRequired()
            .HasMaxLength(255);

            entity
            .HasOne(d => d.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.CategoryId);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImage");

            entity
            .HasOne(d => d.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(d => d.ProductId)
            .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.ToTable("Promotion");

            entity
            .Property(e => e.Description)
            .HasMaxLength(255);

            entity
            .Property(e => e.EndDate)
            .HasColumnType("datetime");

            entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

            entity
            .Property(e => e.StartDate)
            .HasColumnType("datetime");
        });

        modelBuilder.Entity<PromotionCategory>(entity =>
        {
            entity
            .HasNoKey()
            .ToTable("PromotionCategory");

            entity.HasOne(d => d.Category).WithMany()
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PromotionCategory_Categories");

            entity
            .HasOne(d => d.Promotion)
            .WithMany()
            .HasForeignKey(d => d.PromotionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PromotionCategory_Promotion");
        });

        base.OnModelCreating(modelBuilder);
    }
}
