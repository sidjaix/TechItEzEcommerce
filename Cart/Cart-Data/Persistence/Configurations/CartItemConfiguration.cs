using CartCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartData.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems", "bsk");

        builder.HasKey(x => x.Id);

        // Soft reference to Product Domain (Variant)
        builder.Property(x => x.VariantId).IsRequired();

        // Snapshot Data
        builder.Property(x => x.ProductName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.UnitPrice).HasPrecision(18, 2);
        builder.Property(x => x.Quantity).IsRequired();
    }

}
