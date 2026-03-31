using CartCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CartData.Persistence;

public class CartDbContext : DbContext
{

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Applies CartConfiguration and CartItemConfiguration automatically
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
