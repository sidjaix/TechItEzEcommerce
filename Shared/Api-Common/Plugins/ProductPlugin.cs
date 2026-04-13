using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.SemanticKernel;

namespace ApiCommon.Plugins;

/// <summary>
/// Semantic Kernel Plugin that exposes the Product catalog microservice REST API as AI-callable tools.
/// All calls are routed through the YARP API Gateway using the shared "GatewayClient" HttpClient,
/// ensuring gateway policies (auth, rate-limiting, tracing) apply identically to agent calls.
///
/// TOOL-CHAINING ROLE:
/// This plugin is the FIRST step in the add-to-cart tool chain. The agent MUST call
/// search_products → get_product_details to extract the VariantId and Price before it can
/// call CartPlugin.add_item_to_cart. This plugin purposefully never calls the cart — it is
/// read-only and safe to call multiple times for disambiguation.
/// </summary>
[Description(
	"Provides read-only tools to search the product catalog and retrieve full product details " +
	"including Variant IDs and Prices. " +
	"ALWAYS use this plugin BEFORE calling the Cart plugin — you must resolve the exact VariantId " +
	"and UnitPrice from this plugin's tools before any cart operation. " +
	"Never ask the user for a VariantId, ProductId, or any GUID — look them up with these tools."
)]
public sealed class ProductPlugin
{
	private readonly IHttpClientFactory _httpClientFactory;

	private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

	public ProductPlugin(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
	}

	/// <summary>
	/// Performs a semantic (vector similarity) search against the product catalog.
	/// Returns a curated list of matching products with their slugs for use in get_product_details.
	/// </summary>
	[KernelFunction("search_products")]
	[Description(
		"Performs a semantic search of the product catalog using a natural-language description. " +
		"Call this as the FIRST step whenever the user mentions a product by name, description, " +
		"category, brand, or use-case — even if the query is vague (e.g. 'running shoes', 'a fast laptop'). " +
		"\n\nRETURNS: A JSON array of matching products, each with 'Name', 'Slug', 'StartingPrice', " +
		"'Category', 'Brand', and 'Summary'. The 'Slug' field is required input for get_product_details. " +
		"\n\nNEXT STEP: Pass the 'Slug' of the best matching product to get_product_details to retrieve " +
		"the exact VariantId and per-variant Price needed to add an item to the cart. " +
		"\n\nRETURNS ON FAILURE: A plain-English error string. Common causes: catalog service unavailable (503), " +
		"no results found for the given query."
	)]
	public async Task<string> SearchProductsAsync(
		[Description(
			"A natural-language search query describing the product the user is looking for. " +
			"Use the user's own words as closely as possible to maximize semantic relevance. " +
			"Examples: 'blue trail running shoes', 'mechanical keyboard with RGB', 'waterproof hiking jacket'. " +
			"Do NOT pass a VariantId or GUID here — this is a free-text semantic query."
		)]
		string query,

		[Description(
			"The maximum number of results to return. Defaults to 5. " +
			"Use a lower value (e.g. 3) when the user's intent is clear and specific. " +
			"Use a higher value (e.g. 8) when the user is browsing or comparing options."
		)]
		int top = 5,

		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(query))
		{
			return "Invalid query: search query cannot be empty.";
		}

		var client = _httpClientFactory.CreateClient("GatewayClient");

		HttpResponseMessage response;
		try
		{
			response = await client.GetAsync(
				$"/api/catalog/semantic-search?query={Uri.EscapeDataString(query)}",
				cancellationToken);
		}
		catch (HttpRequestException ex)
		{
			return $"Network error while searching the product catalog via the API Gateway: {ex.Message}";
		}

		if (!response.IsSuccessStatusCode)
		{
			var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
			return $"Product catalog search failed. HTTP {(int)response.StatusCode} ({response.ReasonPhrase}): {errorBody}";
		}

		var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);

		// Deserialize and re-serialize a curated, LLM-friendly subset.
		// This avoids returning internal fields and keeps the token count low.
		using var document = JsonDocument.Parse(rawJson);
		var results = new List<object>();

		foreach (var item in document.RootElement.EnumerateArray())
		{
			results.Add(new
			{
				Name = item.TryGetProperty("Name", out var name) ? name.GetString() : null,
				Slug = item.TryGetProperty("Slug", out var slug) ? slug.GetString() : null,
				StartingPrice = item.TryGetProperty("StartingPrice", out var price) ? price.GetDecimal() : 0m,
				Category = item.TryGetProperty("CategoryName", out var cat) ? cat.GetString() : null,
				Brand = item.TryGetProperty("BrandName", out var brand) ? brand.GetString() : null,
				Summary = item.TryGetProperty("ShortSummary", out var summary) ? summary.GetString() : null
			});
		}

		if (results.Count == 0)
		{
			return $"No products found matching '{query}'. Try broadening the search terms.";
		}

		return JsonSerializer.Serialize(results, _jsonOptions);
	}

	/// <summary>
	/// Retrieves full product details for a specific product by its URL slug,
	/// including all variants with their VariantIds, Prices, and attributes.
	/// </summary>
	[KernelFunction("get_product_details")]
	[Description(
		"Retrieves the complete details of a specific product — including ALL variants with their " +
		"exact VariantId (GUID), Price, stock quantity, and human-readable attributes (size, color, etc.) — " +
		"by its URL slug. " +
		"\n\nCall this as the SECOND step after search_products, using the 'Slug' field from the search results. " +
		"\n\nRETURNS: A JSON object with the product 'Name' and a 'Variants' array. Each variant entry contains: " +
		"'VariantId' (the GUID to pass to add_item_to_cart), 'Price' (the UnitPrice to pass to add_item_to_cart), " +
		"'StockQuantity', and 'Attributes' (a JSON string describing size, color, etc.). " +
		"\n\nNEXT STEP: Select the correct variant based on the user's preferences (or ask if ambiguous), " +
		"then pass the 'VariantId', the product 'Name', the variant 'Price', and the desired quantity " +
		"directly to Cart-add_item_to_cart. Do NOT ask the user to confirm the VariantId — it is an internal ID. " +
		"\n\nRETURNS ON FAILURE: A plain-English error string. Common cause: invalid slug (404)."
	)]
	public async Task<string> GetProductDetailsAsync(
		[Description(
			"The URL-safe slug of the product to fetch. This value comes from the 'Slug' field " +
			"in the results of a prior search_products call. " +
			"Example: 'trail-running-shoe-xt200'. " +
			"Never fabricate or guess a slug — always source it from search_products."
		)]
		string slug,

		CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrWhiteSpace(slug))
		{
			return "Invalid slug: product slug cannot be empty. Call search_products first to obtain a valid slug.";
		}

		var client = _httpClientFactory.CreateClient("GatewayClient");

		HttpResponseMessage response;
		try
		{
			response = await client.GetAsync(
				$"/api/catalog/{Uri.EscapeDataString(slug)}",
				cancellationToken);
		}
		catch (HttpRequestException ex)
		{
			return $"Network error while fetching product details via the API Gateway: {ex.Message}";
		}

		if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			return $"Product with slug '{slug}' was not found. Call search_products again with different terms.";
		}

		if (!response.IsSuccessStatusCode)
		{
			var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
			return $"Failed to retrieve product details. HTTP {(int)response.StatusCode} ({response.ReasonPhrase}): {errorBody}";
		}

		var rawJson = await response.Content.ReadAsStringAsync(cancellationToken);

		// Deserialize and re-serialize a curated subset focused on what the LLM needs
		// to call add_item_to_cart: VariantId and Price are the critical fields.
		using var document = JsonDocument.Parse(rawJson);
		var root = document.RootElement;

		var productName = root.TryGetProperty("Name", out var nameEl) ? nameEl.GetString() : slug;

		var variants = new List<object>();
		if (root.TryGetProperty("Variants", out var variantsEl))
		{
			foreach (var variant in variantsEl.EnumerateArray())
			{
				variants.Add(new
				{
					VariantId = variant.TryGetProperty("Id", out var id) ? id.GetGuid() : Guid.Empty,
					Price = variant.TryGetProperty("Price", out var price) ? price.GetDecimal() : 0m,
					StockQuantity = variant.TryGetProperty("StockQuantity", out var stock) ? stock.GetInt32() : 0,
					Attributes = variant.TryGetProperty("AttributesJson", out var attrs) ? attrs.GetString() : "{}"
				});
			}
		}

		if (variants.Count == 0)
		{
			return $"Product '{productName}' was found but has no purchasable variants available.";
		}

		var result = new
		{
			Name = productName,
			Variants = variants
		};

		return JsonSerializer.Serialize(result, _jsonOptions);
	}
}