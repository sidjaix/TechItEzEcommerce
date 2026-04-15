using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
#pragma warning disable SKEXP0070
using Microsoft.SemanticKernel.Connectors.Ollama;
#pragma warning restore SKEXP0070
using System.ComponentModel.DataAnnotations;

namespace AiApi.Controllers;

/// <summary>
/// The central AI agent endpoint. Receives a natural-language prompt from an
/// authenticated user and autonomously orchestrates tool calls (via Semantic
/// Kernel function calling) to fulfil the user's intent.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class ChatController : ControllerBase
{
	private readonly Kernel _kernel;
	private readonly IChatCompletionService _chatCompletion;
	private readonly ILogger<ChatController> _logger;

	/// <summary>
	/// System prompt that defines the agent's persona, scope, and tool-use rules.
	/// Keeping this as a constant makes it easy to iterate without touching logic.
	/// </summary>
	private const string SystemPrompt =
		"You are a helpful e-commerce shopping assistant for the TechItEz store. " +
		"Your job is to help authenticated customers find products and manage their shopping cart. " +

		"\n\nCORE RULES:" +
		"\n1. You MUST use the available tools to perform real actions — never simulate or pretend to add items." +

		"\n\n2. ADD-TO-CART PROTOCOL — MANDATORY TOOL-CHAINING SEQUENCE:" +
		"\n   When a user asks to add, buy, or purchase any product, you MUST execute ALL of the following " +
		"steps autonomously and silently, without asking the user for any technical identifiers:" +
		"\n   STEP 1 — Call 'Product-search_products' with the user's natural-language product description " +
		"to find matching products. Extract the 'Slug' of the best match." +
		"\n   STEP 2 — Call 'Product-get_product_details' with that Slug to retrieve the full list of variants " +
		"including their exact VariantId (GUID), Price, and Attributes (size, color, etc.)." +
		"\n   STEP 3 — Select the correct variant. If the user specified a size or color, match it from " +
		"the Attributes. If multiple variants exist and the user did not specify, ask ONE clarifying question " +
		"using human-readable terms only (e.g. 'Which size would you like?') — never mention GUIDs." +
		"\n   STEP 4 — Call 'Cart-add_item_to_cart' with the VariantId, the product Name, the variant Price, " +
		"and the requested Quantity. Complete this call before replying to the user." +

		"\n\n3. NEVER ask the user for a VariantId, ProductId, or any GUID or internal technical identifier. " +
		"These are implementation details that you MUST resolve autonomously using the Product tools." +

		"\n\n4. Always confirm success in your final reply with the exact product name, variant attributes, " +
		"quantity, and price that were added to the cart." +

		"\n\n5. If any tool call fails, explain the error in plain English, state which step failed, " +
		"and suggest a corrective action (e.g. try a different search query)." +

		"\n\n6. Stay focused on shopping tasks. Politely decline unrelated requests.";

	public ChatController(Kernel kernel, IChatCompletionService chatCompletion, ILogger<ChatController> logger)
	{
		_kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
		_chatCompletion = chatCompletion ?? throw new ArgumentNullException(nameof(chatCompletion));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	/// <summary>
	/// Accepts a natural-language prompt and returns the AI agent's response.
	/// The agent may autonomously call registered Semantic Kernel plugins (e.g. CartPlugin)
	/// to fulfil the user's intent before producing the final reply.
	/// </summary>
	/// <remarks>
	/// Example request body:
	/// <code>{ "prompt": "Add the cheapest mechanical keyboard to my cart" }</code>
	/// </remarks>
	/// <param name="request">The user's chat request.</param>
	/// <param name="cancellationToken">Request cancellation token.</param>
	/// <returns>The agent's final reply.</returns>
	[HttpPost]
	[ProducesResponseType(typeof(ChatResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	public async Task<IActionResult> Chat(
		[FromBody] ChatRequest request,
		CancellationToken cancellationToken)
	{
		_logger.LogInformation("Chat request received. Prompt length: {Length}", request.Prompt.Length);

		// Build a fresh chat history per request.
		// The system prompt anchors the agent's behaviour for every conversation turn.
		var chatHistory = new ChatHistory();
		chatHistory.AddSystemMessage(SystemPrompt);
		chatHistory.AddUserMessage(request.Prompt);

		// Configure automatic function calling — SK will loop internally, calling tools
		// as requested by the LLM and feeding results back, until the LLM produces a
		// final text response with no further tool calls.
#pragma warning disable SKEXP0070
		var executionSettings = new OllamaPromptExecutionSettings
		{
			FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
			// Try setting a slightly higher temperature to encourage natural language generation
			Temperature = 0.7f
		};
#pragma warning restore SKEXP0070

		ChatMessageContent result;
		try
		{
			result = await _chatCompletion.GetChatMessageContentAsync(
				chatHistory,
				executionSettings,
				_kernel,
				cancellationToken);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Semantic Kernel chat completion failed for prompt: {Prompt}", request.Prompt);
			return StatusCode(StatusCodes.Status502BadGateway, new
			{
				error = "The AI service encountered an error while processing your request.",
				detail = ex.Message
			});
		}

		var reply = result.Content ?? "I was unable to generate a response. Please try again.";

		_logger.LogInformation("Chat response generated. Reply length: {Length}", reply.Length);

		return Ok(new ChatResponse(reply));
	}
}

/// <summary>
/// The request body for <c>POST /api/chat</c>.
/// </summary>
/// <param name="Prompt">The user's natural-language message.</param>
public sealed record ChatRequest(
	[Required]
	[MinLength(1, ErrorMessage = "Prompt cannot be empty.")]
	[MaxLength(2000, ErrorMessage = "Prompt must not exceed 2000 characters.")]
	string Prompt
);

/// <summary>
/// The response body from <c>POST /api/chat</c>.
/// </summary>
/// <param name="Reply">The agent's final natural-language reply.</param>
public sealed record ChatResponse(string Reply);