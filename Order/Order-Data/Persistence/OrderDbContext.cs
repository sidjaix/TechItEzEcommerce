using Microsoft.EntityFrameworkCore;

namespace OrderData.Persistence;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


    }
}
