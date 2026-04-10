using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCore.Entities;

namespace ProductData.Persistence.Configurations;

public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
{
	public void Configure(EntityTypeBuilder<CatalogItem> builder)
	{
		builder.ToTable("CatalogItems", "cat");

		builder.HasKey(x => x.Id);
		builder.Property(b => b.Id).HasDefaultValueSql("newsequentialid()");

		builder.Property(x => x.Slug).IsRequired().HasMaxLength(150);
		builder.HasIndex(x => x.Slug).IsUnique(); // Crucial for fast URL lookups

		builder.Property(x => x.BaseName).IsRequired().HasMaxLength(200);
		builder.Property(x => x.ShortSummary).HasMaxLength(500);

		// AI Fuel: Explicitly map as max length text
		builder.Property(x => x.SemanticDescription).HasColumnType("nvarchar(max)");

		// Relationships: Foreign Keys to Taxonomy

		builder.HasOne(x => x.Category)
		.WithMany()
		.HasForeignKey(x => x.CategoryId)
		.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(x => x.Brand)
		.WithMany()
		.HasForeignKey(x => x.BrandId)
		.OnDelete(DeleteBehavior.Restrict);

		// Relationships: Child Collections
		builder.HasMany(x => x.Variants)
			   .WithOne()
			   .HasForeignKey(v => v.CatalogItemId)
			   .OnDelete(DeleteBehavior.Cascade); // Deleting catalog item deletes variants

		builder.HasMany(x => x.Reviews)
			   .WithOne()
			   .HasForeignKey(r => r.CatalogItemId)
			   .OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(x => x.Tags)
			   .WithOne()
			   .HasForeignKey(t => t.CatalogItemId)
			   .OnDelete(DeleteBehavior.Cascade);
	}
}
