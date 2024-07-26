using Microsoft.EntityFrameworkCore;
using User_Core.Entities;

namespace User_Core;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderCoupon> OrderCoupons { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ContactUs> ContactUs { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .Property(b => b.IsActive)
        .HasDefaultValue(true);
        modelBuilder.Entity<User>()
        .Property(b => b.CreatedOn)
        .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<User>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Category>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Category>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Product>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Product>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Address>()
        .Property(b => b.CreatedOn)
        .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Address>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Address>()
        .Property(b => b.IsShippingAddress)
        .HasDefaultValue(true);

        modelBuilder.Entity<ContactUs>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<ContactUs>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Coupon>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<Coupon>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Order>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Payment>()
        .Property(b => b.PaymentDate)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<ProductImage>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<ProductImage>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<ProductReview>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<ProductImage>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<UserRole>()
       .Property(b => b.CreatedOn)
       .HasDefaultValueSql("getdate()");
        modelBuilder.Entity<UserRole>()
        .Property(b => b.LastModifiedOn)
        .HasDefaultValueSql("getdate()");

        base.OnModelCreating(modelBuilder);
    }
}
