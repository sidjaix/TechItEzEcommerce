using Cart_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart_Core;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options)
          : base(options)
    {
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_CartItem_CartId");

            entity
            .HasOne(d => d.Cart)
            .WithMany(p => p.CartItems)
            .HasForeignKey(d => d.CartId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_Cart_UserId");
        });

        base.OnModelCreating(modelBuilder);
    }

}
