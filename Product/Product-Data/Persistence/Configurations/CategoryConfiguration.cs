using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCore.Entities;

namespace ProductData.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable("Categories", "cat");

		builder.HasKey(x => x.Id);
		builder.Property(b => b.Id).HasDefaultValueSql("newsequentialid()");

		builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
		builder.Property(x => x.Description).HasMaxLength(1000);

		// Self-referencing relationship
		builder.HasOne(x => x.ParentCategory)
			   .WithMany(x => x.SubCategories)
			   .HasForeignKey(x => x.ParentCategoryId)
			   .OnDelete(DeleteBehavior.Restrict); // Strict requirement for SQL Server
	}

}
