using ApiCommon.Contracts;
using ApiCommon.Data;
using MassTransit;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

namespace ProductApi.Consumers;

public class CatalogItemSemanticProfileUpdatedConsumer(
	IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
	VectorStoreCollection<Guid, ProductRecord> vectorStore,
	ILogger<CatalogItemSemanticProfileUpdatedConsumer> logger) : IConsumer<CatalogItemSemanticProfileUpdatedEvent>
{
	public async Task Consume(ConsumeContext<CatalogItemSemanticProfileUpdatedEvent> context)
	{
		var evt = context.Message;

		logger.LogInformation("Received Semantic Profile Update for ProductId: {ProductId}", evt.ProductId);

		try
		{
			// 1. Combine Name, Summary, and Description into a rich text string
			var combinedText = $"""
                Product Name: {evt.BaseName}
                Summary: {evt.ShortSummary}
                Description: {evt.SemanticDescription}
                """;

			// 2. Generate vector embedding
			var embedding = await embeddingGenerator.GenerateAsync(combinedText, cancellationToken: context.CancellationToken);

			// 3. Create the ProductRecord to upsert
			var productRecord = new ProductRecord
			{
				Id = evt.ProductId,
				Name = evt.BaseName,
				Description = combinedText,
				Vector = embedding.Vector
			};

			// 4. Upsert into Qdrant using Semantic Kernel abstractions
			await vectorStore.UpsertAsync(productRecord, cancellationToken: context.CancellationToken);

			logger.LogInformation("Successfully upserted vector embedding for ProductId: {ProductId}", evt.ProductId);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to generate or upsert vector embedding for ProductId: {ProductId}", evt.ProductId);
			throw; // Re-throw to allow MassTransit to handle retries/DLQ
		}
	}
}