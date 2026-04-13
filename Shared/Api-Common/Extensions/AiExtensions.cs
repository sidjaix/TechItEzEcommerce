using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.VectorData;
using ApiCommon.Data;
using ApiCommon.Handlers;
using ApiCommon.Plugins;
using Microsoft.SemanticKernel.Connectors.Qdrant;

namespace ApiCommon.Extensions;

public static class AiExtensions
{
	/// <summary>
	/// Extension method to add and configure Semantic Kernel with Ollama, Qdrant Vector Store,
	/// and the gateway-backed Semantic Kernel Plugins (e.g. CartPlugin).
	/// </summary>
	/// <param name="services">The service collection to configure.</param>
	/// <param name="configuration">The application configuration (appsettings).</param>
	/// <returns>The updated service collection.</returns>
	public static IServiceCollection AddSemanticKernelWithOllama(this IServiceCollection services, IConfiguration configuration)
	{
		// Setup Ollama (Running locally via Docker)
		var ollamaEndpoint = new Uri(configuration["Ollama:Endpoint"] ?? "http://ollama:11434");

		// CONFIGURE TIMEOUT: Register a named HttpClient for Ollama
		// We increase this to 5 minutes to allow for multi-step tool chaining (Search -> Cart)
		services.AddHttpClient("OllamaClient", client =>
		{
			client.BaseAddress = ollamaEndpoint;
			client.Timeout = TimeSpan.FromMinutes(5);
		});

#pragma warning disable SKEXP0070
		// 1. Register Ollama embedding generator (nomic-embed-text for vector search)
		services.AddOllamaEmbeddingGenerator(
			modelId: "nomic-embed-text:v1.5",
			endpoint: ollamaEndpoint
		);

		// 2. Add Chat Completion and build the Kernel
		var kernelBuilder = services.AddKernel();
		kernelBuilder.AddOllamaChatCompletion(
			modelId: "qwen2.5",
			endpoint: ollamaEndpoint
		);
		kernelBuilder.AddOllamaChatCompletion(
			modelId: "qwen2.5",
			// We pull the configured HttpClient from the ServiceProvider
			httpClient: services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>().CreateClient("OllamaClient")
		);


		// 3. Modern Qdrant Registration (Use gRPC port 6334)
		services.AddQdrantVectorStore(
			host: configuration["Qdrant:Host"] ?? "qdrant",
			port: 6334,
			https: false
		);

		// 4. Register the specific Product Collection
		// Note the change from 'IVectorStore' to 'VectorStore'
		services.AddScoped(sp =>
		{
			var vectorStore = sp.GetRequiredService<VectorStore>();
			return vectorStore.GetCollection<Guid, ProductRecord>("products");
		});

		// 5. Register TokenDelegatingHandler so it can be attached to the gateway HttpClient.
		//    This handler reads the Bearer JWT from the current IHttpContextAccessor and forwards
		//    it on every outgoing request, making the agent appear as the authenticated user.
		services.AddTransient<TokenDelegatingHandler>();

		// 6. Register the named "GatewayClient" HttpClient.
		//    All Semantic Kernel Plugin HTTP calls are routed through the YARP API Gateway
		//    (not directly to individual microservices) to honour gateway policies identically
		//    to a real browser user. The base address is read from configuration key "Gateway:BaseUrl".
		services.AddHttpClient("GatewayClient", client =>
		{
			client.BaseAddress = new Uri(configuration["Gateway:BaseUrl"] ?? "http://api_gateway:8080");
		})
		.AddHttpMessageHandler<TokenDelegatingHandler>();

		// 7. Import CartPlugin into the Kernel so the LLM can invoke cart operations as tools.
		//    CartPlugin is DI-resolved, meaning IHttpClientFactory is injected automatically.
		kernelBuilder.Plugins.AddFromType<CartPlugin>("Cart");

		// 8. Import ProductPlugin — enables the LLM to search the catalog and resolve
		//    VariantId + Price autonomously before calling CartPlugin.add_item_to_cart.
		//    This is the first link in the tool-chaining pipeline:
		//    search_products → get_product_details → add_item_to_cart.
		kernelBuilder.Plugins.AddFromType<ProductPlugin>("Product");

		return services;
	}
}