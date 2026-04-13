using System.ComponentModel;
using System.Net.Http.Json;
using Microsoft.SemanticKernel;

namespace ApiCommon.Plugins;

/// <summary>
/// Semantic Kernel Plugin that exposes the Cart microservice REST API as AI-callable tools.
/// All calls are routed through the YARP API Gateway, identical to a real user browser request.
/// The authenticated user's identity is automatically derived from the Bearer JWT token
/// forwarded by the TokenDelegatingHandler — the agent never needs to manage user sessions directly.
/// </summary>
[Description(
	"Provides tools to manage the authenticated user's shopping cart. " +
	"All operations are executed against the live Cart microservice via the API Gateway. " +
	"The agent MUST have a valid Bearer JWT token in the current HTTP context before using any tool in this plugin."
)]
public sealed class CartPlugin
{
	private readonly IHttpClientFactory _httpClientFactory;

	// Local mirror of CartController.AddItemRequest — keeps Api-Common free of Cart-Application project references.
	// IMPORTANT: Property names and types MUST match CartController.AddItemRequest exactly so model binding succeeds.
	private sealed record AddItemRequest(Guid VariantId, string ProductName, decimal UnitPrice, int Quantity);

	public CartPlugin(IHttpClientFactory httpClientFactory)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
	}

	/// <summary>
	/// Adds a specific product variant to the authenticated user's shopping cart.
	/// </summary>
	[KernelFunction("add_item_to_cart")]
	[Description(
		"Adds a specific product variant to the authenticated user's shopping cart by issuing a " +
		"POST request to /api/cart/AddItem via the API Gateway. " +
		"\n\nUSE THIS TOOL WHEN: The user asks to add a product to their cart, basket, or shopping bag, " +
		"or when the user says 'buy', 'purchase', or 'add' in the context of a product they have already selected. " +
		"\n\nPRE-CONDITION — VariantId: You MUST call the Product catalog tool first to retrieve the exact " +
		"VariantId (a GUID) for the chosen product variant (size, color, etc.). Do NOT guess, invent, or " +
		"hardcode a VariantId. If the user has not specified a variant and multiple exist, ask for clarification " +
		"before calling this tool. " +
		"\n\nPRE-CONDITION — Authentication: The HTTP context must carry a valid Bearer JWT token for the " +
		"logged-in customer. This token is forwarded automatically; do not pass any user ID or session data. " +
		"\n\nBEHAVIOR ON SUCCESS: The cart is created automatically if it does not exist. If the same VariantId " +
		"is added again, the quantity is incremented (upsert semantics). " +
		"\n\nBEHAVIOR ON FAILURE: Returns a descriptive error string. Common causes: invalid VariantId (404), " +
		"unauthenticated request (401), gateway unavailable (503). " +
		"\n\nRETURNS: A plain-English confirmation string stating what was added, or an error message."
	)]
	public async Task<string> AddItemToCartAsync(
		[Description(
			"The unique GUID identifier of the specific product variant to add to the cart. " +
			"This value comes from the Product catalog API (e.g., from the 'VariantId' field on a ProductVariant). " +
			"Example: 'd3b07384-d9a3-4c1e-9b2c-1a2b3c4d5e6f'. " +
			"NEVER fabricate this value — always source it from a prior catalog lookup."
		)]
		Guid variantId,

		[Description(
			"The human-readable display name of the product as it appears in the catalog. " +
			"This is stored on the cart line item for display purposes in the UI. " +
			"Example: 'Trail Running Shoe - Blue / Size 10'. " +
			"Use the exact name returned by the Product catalog API."
		)]
		string productName,

		[Description(
			"The exact current selling price of the variant, in decimal format, without a currency symbol. " +
			"This price is locked into the cart line item at the time of addition and is used to calculate " +
			"the cart total. Source this value from the Product catalog API — do not ask the user for a price. " +
			"Example: 129.99"
		)]
		decimal unitPrice,

		[Description(
			"The number of units of this variant the user wants to add to their cart. " +
			"Must be a positive integer (minimum 1). If the user says 'a pair', interpret as 2. " +
			"If the user does not specify a quantity, default to 1. " +
			"Example: 1"
		)]
		int quantity,

		CancellationToken cancellationToken = default)
	{
		if (quantity <= 0)
		{
			return $"Invalid quantity '{quantity}'. Quantity must be a positive integer of at least 1.";
		}

		var client = _httpClientFactory.CreateClient("GatewayClient");

		var payload = new AddItemRequest(variantId, productName, unitPrice, quantity);

		HttpResponseMessage response;
		try
		{
			response = await client.PostAsJsonAsync("/api/cart/AddItem", payload, cancellationToken);
		}
		catch (HttpRequestException ex)
		{
			return $"Network error while contacting the Cart service via the API Gateway: {ex.Message}";
		}

		if (response.IsSuccessStatusCode)
		{
			return $"Successfully added {quantity}x '{productName}' (VariantId: {variantId}, Unit Price: {unitPrice:C}) to the cart.";
		}

		var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
		return $"Failed to add '{productName}' to cart. HTTP {(int)response.StatusCode} ({response.ReasonPhrase}): {errorBody}";
	}
}