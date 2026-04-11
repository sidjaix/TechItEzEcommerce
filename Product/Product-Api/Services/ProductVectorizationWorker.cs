using ApiCommon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using ProductData.Persistence;

namespace ProductApi.Services;

/// <summary>
/// A background worker that vectorizes active products on startup and saves them to Qdrant.
/// </summary>
public class ProductVectorizationWorker(
	IServiceProvider serviceProvider,
	IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
	ILogger<ProductVectorizationWorker> logger) : BackgroundService
{

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		logger.LogInformation("Product Vectorization Worker starting...");

		// Wait a bit to ensure Qdrant and DB are fully up
		await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

		try
		{
			using var scope = serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
			var vectorStore = scope.ServiceProvider.GetRequiredService<VectorStore>();

			var collection = vectorStore.GetCollection<Guid, ProductRecord>("products");
			await collection.EnsureCollectionExistsAsync(stoppingToken);

			logger.LogInformation("Fetching active products from database...");

			// Adhering to Constraints: Go through existing EF Core ProductDbContext
			var products = await dbContext.CatalogItems
				.Where(p => p.IsPublished)
				.ToListAsync(stoppingToken);

			logger.LogInformation($"Found {products.Count} active products to vectorize.");

			foreach (var product in products)
			{
				if (stoppingToken.IsCancellationRequested) break;

				// Skip if description is totally empty
				var descriptionText = product.SemanticDescription;
				if (string.IsNullOrWhiteSpace(descriptionText))
				{
					descriptionText = product.ShortSummary ?? product.BaseName;
				}

				if (string.IsNullOrWhiteSpace(descriptionText)) continue;

				var textToVectorize = $"Product Name: {product.BaseName}\nDescription: {descriptionText}";

				try
				{
					// Generate embedding for the product's text
					var embeddingResult = await embeddingGenerator.GenerateAsync(textToVectorize, cancellationToken: stoppingToken);

					var vectorModel = new ProductRecord
					{
						Id = product.Id,
						Name = product.BaseName,
						Description = descriptionText,
						Vector = embeddingResult.Vector // Extract the vector (The .Vector property is a ReadOnlyMemory<float>)
					};

					await collection.UpsertAsync(vectorModel, cancellationToken: stoppingToken);
					logger.LogInformation($"Successfully vectorized product: {product.BaseName} ({product.Id})");
				}
				catch (Exception ex)
				{
					logger.LogError(ex, $"Failed to vectorize product {product.Id}");
				}
			}

			logger.LogInformation("Product Vectorization Worker completed successfully.");
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "A fatal error occurred in the Product Vectorization Worker.");
		}
	}
}