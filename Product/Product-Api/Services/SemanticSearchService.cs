using ApiCommon.Data;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using ProductApplication.Interfaces;

namespace ProductApi.Services;

public class SemanticSearchService(
	IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
	VectorStoreCollection<Guid, ProductRecord> vectorStore,
	ILogger<SemanticSearchService> logger) : ISemanticSearchService
{
	public async Task<IEnumerable<Guid>> SearchProductIdsAsync(string query, int top = 5, CancellationToken cancellationToken = default)
	{
		try
		{
			logger.LogInformation("Generating embeddings for query: {Query}", query);

			// 1. Convert user text intent to vector embeddings
			var queryEmbedding = await embeddingGenerator.GenerateAsync(query, cancellationToken: cancellationToken);

			// 2. Perform Cosine Similarity search in Qdrant
			var searchOptions = new VectorSearchOptions<ProductRecord>
			{
				IncludeVectors = false
			};

			var searchResults = vectorStore.SearchAsync(queryEmbedding.Vector, top, searchOptions, cancellationToken);

			var productIds = new List<Guid>();

			await foreach (var result in searchResults.WithCancellation(cancellationToken))
			{
				productIds.Add(result.Record.Id);
			}

			return productIds;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to execute semantic search for query: {Query}", query);
			return [];
		}
	}
}