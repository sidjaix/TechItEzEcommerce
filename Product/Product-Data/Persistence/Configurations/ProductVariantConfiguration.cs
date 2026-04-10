using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCore.Entities;

namespace ProductData.Persistence.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
	public void Configure(EntityTypeBuilder<ProductVariant> builder)
	{
		builder.ToTable("ProductVariants", "cat");

		builder.HasKey(x => x.Id);
		builder.Property(b => b.Id).HasDefaultValueSql("newsequentialid()");

		builder.Property(x => x.Sku).IsRequired().HasMaxLength(100);
		builder.HasIndex(x => x.Sku).IsUnique();

		// Standard financial precision for SQL Server
		builder.Property(x => x.Price).HasPrecision(18, 2);

		// Store the dynamic attributes as a JSON string
		builder.Property(x => x.AttributesJson).HasColumnType("nvarchar(max)");

		builder.HasMany(x => x.Images)
			   .WithOne()
			   .HasForeignKey(i => i.ProductVariantId)
			   .OnDelete(DeleteBehavior.Cascade);
	}

}
