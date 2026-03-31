using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderCore.Entities;

namespace OrderData.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Isolate to the Order schema
        builder.ToTable("Orders", "ord");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.OrderDate).IsRequired();
        builder.Property(x => x.Status).HasConversion<int>(); // Store enum as INT

        // Complex Type / Value Object Mapping
        builder.OwnsOne(x => x.ShippingAddress, a =>
        {
            a.Property(p => p.Street).HasMaxLength(200).IsRequired();
            a.Property(p => p.City).HasMaxLength(100).IsRequired();
            a.Property(p => p.State).HasMaxLength(100).IsRequired();
            a.Property(p => p.Country).HasMaxLength(100).IsRequired();
            a.Property(p => p.ZipCode).HasMaxLength(20).IsRequired();
        });

        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(i => i.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }

}
