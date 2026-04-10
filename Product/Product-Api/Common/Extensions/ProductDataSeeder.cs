
using Bogus;
using Microsoft.EntityFrameworkCore;
using ProductCore.Entities;
using ProductData.Persistence;
using System.Text.Json;

namespace ProductApi.Common.Extensions;

public static class ProductDataSeeder
{
	public static async Task SeedAsync(ProductDbContext _context)
	{
		// Only seed if the database is completely empty
		if (await _context.CatalogItems.AnyAsync())
			return;

		// 1. Seed Brands
		var brandFaker = new Faker<Brand>()
			.CustomInstantiator(f => new Brand(
				name: $"{f.Company.CompanyName()} {f.Company.CompanySuffix()}", // AI Fuel: More realistic brand names,
				description: f.Lorem.Paragraphs(2) // AI Fuel: Rich company history
			));
		var brands = brandFaker.Generate(10);
		await _context.Brands.AddRangeAsync(brands);

		// 2. Seed Top-Level Categories
		var categoryFaker = new Faker<Category>()
			.CustomInstantiator(f => new Category(
				name: f.Commerce.Categories(1)[0],
				description: f.Commerce.ProductDescription(),
				parentCategoryId: null
			));
		var topCategories = categoryFaker.Generate(5);
		await _context.Categories.AddRangeAsync(topCategories);

		// 3. Seed Catalog Items (The Aggregate Roots)
		var catalogItemFaker = new Faker<CatalogItem>()
			.CustomInstantiator(f =>
			{
				var name = f.Commerce.ProductName();
				var description = f.Commerce.ProductDescription();
				var adjective = f.Commerce.ProductAdjective();
				var material = f.Commerce.ProductMaterial();
				var color = f.Commerce.Color();

				var item = new CatalogItem(
					baseName: name,
					slug: name + "-" + color,
					categoryId: f.PickRandom(topCategories).Id,
					brandId: f.PickRandom(brands).Id
				);


				// Simulate the business logic of adding semantic data
				// AI Fuel: Massive, multi-paragraph markdown descriptions
				var semanticText = $"## Overview\n{description}\n\n## Features\n{adjective}\n## Material\n{material}\n ## Color\n {color}";

				// Reflection used here just to bypass the private setter for seeding purposes
				typeof(CatalogItem).GetProperty("ShortSummary")?.SetValue(item, adjective + " " + material);
				typeof(CatalogItem).GetProperty("SemanticDescription")?.SetValue(item, semanticText);
				typeof(CatalogItem).GetProperty("IsPublished")?.SetValue(item, true);

				return item;
			});

		var catalogItems = catalogItemFaker.Generate(50); // Generate 50 base products

		// 4. Seed Variants, Images, Reviews, and Tags per Catalog Item
		foreach (var item in catalogItems)
		{
			// Add 1 to 4 variants per product
			var variantCount = new Faker().Random.Int(1, 4);
			for (int i = 0; i < variantCount; i++)
			{
				var attributes = new { Color = new Faker().Commerce.Color(), Size = new Faker().PickRandom(new[] { "S", "M", "L", "XL" }) };

				var variant = new ProductVariant(
					catalogItemId: item.Id,
					sku: new Faker().Commerce.Ean13(),
					price: decimal.Parse(new Faker().Commerce.Price(10, 1000)),
					stockQuantity: new Faker().Random.Int(0, 100),
					attributesJson: JsonSerializer.Serialize(attributes)
				);

				// Reflection to bypass encapsulated collections for seeding
				var variantsList = (List<ProductVariant>)typeof(CatalogItem).GetField("_variants", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(item);
				variantsList.Add(variant);

				// Add Images to Variant
				var imagesList = (List<ProductImage>)typeof(ProductVariant).GetField("_images", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(variant);
				imagesList.Add(new ProductImage(productVariantId: variant.Id, imageUrl: new Faker().Image.PicsumUrl(), altText: new Faker().Commerce.ProductDescription(), isPrimary: true, displayOrder: 1));
			}

			// Add 0 to 10 Reviews per product (Crucial for natural language AI context)
			var reviewCount = new Faker().Random.Int(0, 10);
			var reviewsList = (List<ProductReview>)typeof(CatalogItem).GetField("_reviews", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(item);
			for (int i = 0; i < reviewCount; i++)
			{
				reviewsList.Add(new ProductReview(
					catalogItemId: item.Id,
					customerId: Guid.NewGuid(), // Mocked external user
					rating: new Faker().Random.Int(1, 5),
					reviewerAlias: new Faker().Internet.UserName(),
					reviewText: new Faker().Rant.Review() // Generates realistic, opinionated text
				));
			}

			// Add Tags
			var tagsList = (List<ProductTag>)typeof(CatalogItem).GetField("_tags", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(item);
			tagsList.Add(new ProductTag(name: new Faker().Commerce.ProductAdjective(), catalogItemId: item.Id));
			tagsList.Add(new ProductTag(name: new Faker().Commerce.ProductMaterial(), catalogItemId: item.Id));
		}

		await _context.CatalogItems.AddRangeAsync(catalogItems);
		await _context.SaveChangesAsync();
	}

	public static async Task InitializeDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var _context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();

		try
		{
			// 1. Apply any pending EF Core Migrations automatically
			if (_context.Database.IsSqlServer() && _context.Database.GetPendingMigrations().Any())
			{
				await _context.Database.MigrateAsync();
			}

			// 2. Seed the Bogus data
			await SeedAsync(_context);
		}
		catch (Exception ex)
		{
			var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred while migrating or seeding the database.");
		}
	}

}
