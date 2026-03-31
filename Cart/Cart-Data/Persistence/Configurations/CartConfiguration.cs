using CartCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CartData.Persistence.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        // Isolate to the basket schema
        builder.ToTable("Carts", "bsk");

        builder.HasKey(x => x.Id);

        // Soft reference to User Domain
        builder.Property(x => x.CustomerId).IsRequired();

        // A customer can only have one active cart at a time
        builder.HasIndex(x => x.CustomerId).IsUnique();

        // Relationship: One Cart -> Many CartItems
        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(i => i.CartId)
               .OnDelete(DeleteBehavior.Cascade); // Deleting the cart wipes the items
    }

}
