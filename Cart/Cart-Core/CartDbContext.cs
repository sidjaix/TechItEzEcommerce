using Cart_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cart_Core;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options)
          : base(options)
    {
    }

    public DbSet<UserCart> ShoppingCarts { get; set; }
    public DbSet<UserCartItem> ShoppingCartItems { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}
