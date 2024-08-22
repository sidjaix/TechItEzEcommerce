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
            entity.HasKey(e => e.CartItemId);

            entity.Property(o => o.CartItemId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1); // Start with 1, increment by 1

            entity
            .HasOne(d => d.Cart)
            .WithMany(p => p.CartItems)
            .HasForeignKey(d => d.CartId);

            entity.HasIndex(e => e.CartId, "IX_CartItem_CartId");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.Property(o => o.CartId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1); // Start with 1, increment by 1

            entity.HasIndex(e => e.CartId, "IX_Cart_UserId");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Wishlist_UserId");
        });

        modelBuilder.Entity<WishlistItem>(entity =>
        {
            entity.HasKey(e => e.WishlistItemId);

            entity.Property(o => o.WishlistItemId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1); // Start with 1, increment by 1

            entity
            .HasOne(d => d.Wishlist)
            .WithMany(p => p.WishlistItems)
            .HasForeignKey(d => d.WishlistId);

            entity.HasIndex(e => e.WishlistId, "IX_WishlistItem_WishlistId");
        });

        base.OnModelCreating(modelBuilder);
    }
}
