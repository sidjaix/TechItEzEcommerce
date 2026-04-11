using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.VectorData;
using ApiCommon.Data;
using Microsoft.SemanticKernel.Connectors.Qdrant;

namespace ApiCommon.Extensions;

public static class AiExtensions
{
	/// <summary>
	/// Extension method to add and configure Semantic Kernel with Ollama and Qdrant Vector Store.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddSemanticKernelWithOllama(this IServiceCollection services, IConfiguration configuration)
	{
		// // Setup Ollama(Running locally via Docker)
		var ollamaEndpoint = new Uri(configuration["Ollama:Endpoint"] ?? "http://ollama:11434");
#pragma warning disable SKEXP0070
		// 1. New Pattern: Use the OllamaApiClient as the foundation
		// This is now the "proper" way to register Ollama services
		services.AddOllamaEmbeddingGenerator(
			modelId: "nomic-embed-text:v1.5",
			endpoint: ollamaEndpoint
		);

		// 2. Add Chat Completion (Uses the same client under the hood)
		var kernelBuilder = services.AddKernel();
		kernelBuilder.AddOllamaChatCompletion(
			modelId: "llama3",
			endpoint: ollamaEndpoint
		);

		// 3. Modern Qdrant Registration (Use gRPC port 6334)
		services.AddQdrantVectorStore(
			host: configuration["Qdrant:Host"] ?? "qdrant",
			port: 6334,
			https: false
		// Note: The new registration pattern for the vector store options is to configure them via the collection retrieval, not at the global level.
		// options: new QdrantVectorStoreOptions
		// {
		// 	HasNamedVectors = true,
		// }
		);

		// 4. Register the specific Product Collection
		// Note the change from 'IVectorStore' to 'VectorStore'
		services.AddScoped(sp =>
		{
			var vectorStore = sp.GetRequiredService<VectorStore>();
			return vectorStore.GetCollection<Guid, ProductRecord>("products");
		});

		return services;
	}
}