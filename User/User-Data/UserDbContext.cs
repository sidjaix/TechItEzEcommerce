using Microsoft.EntityFrameworkCore;
using User_Core.Entities;

namespace User_Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options)
          : base(options)
    {
    }

    public DbSet<User> Users { get; }
    public DbSet<Role> Roles { get; }
    public DbSet<UserRole> UserRoles { get; }
    public DbSet<Address> Addresses { get; }
    public DbSet<Category> Categories { get; }
    public DbSet<Product> Products { get; }
    public DbSet<Order> Orders { get; }
    public DbSet<OrderCoupon> OrderCoupons { get; }
    public DbSet<Coupon> Coupons { get; }
    public DbSet<ProductImage> ProductImages { get; }
    public DbSet<ProductReview> ProductReviews { get; }
    public DbSet<ShoppingCart> ShoppingCarts { get; }
    public DbSet<Payment> Payments { get; }
    public DbSet<ContactUs> ContactUs { get; }
    public DbSet<Wishlist> Wishlists { get; }
    public DbSet<OrderDetail> OrderDetails { get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
