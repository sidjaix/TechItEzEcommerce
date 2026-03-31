using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderCore.Entities;

namespace OrderData.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems", "ord");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.VariantId).IsRequired();
        builder.Property(x => x.ProductName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.Quantity).IsRequired();
    }
}
