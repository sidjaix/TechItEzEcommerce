using Microsoft.EntityFrameworkCore;

namespace CartData.Persistence;

public class CartDbContext : DbContext
{
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
