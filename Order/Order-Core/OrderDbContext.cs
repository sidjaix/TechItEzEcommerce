using Microsoft.EntityFrameworkCore;
using Order_Core.Entities;

namespace Order_Core;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
          : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(x => x.OrderStatusId);

            entity.Property(o => o.OrderStatusId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1); // Start with 1, increment by 1
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.OrderId);

            entity.Property(o => o.OrderId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1); // Start with 1, increment by 1
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(x => x.OrderDetialId);

            entity.Property(o => o.OrderDetialId)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(1, 1); // Start with 1, increment by 1

            entity
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.OrderId);

            entity.HasIndex(e => e.OrderId, "IX_OrderDetail_OrderId");
        });
        base.OnModelCreating(modelBuilder);
    }
}
