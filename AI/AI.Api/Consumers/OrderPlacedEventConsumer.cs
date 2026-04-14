using ApiCommon.Contracts;
using MassTransit;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
#pragma warning disable SKEXP0070
using Microsoft.SemanticKernel.Connectors.Ollama;
#pragma warning restore SKEXP0070

namespace AiApi.Consumers;

/// <summary>
/// Reacts autonomously to an <see cref="OrderPlacedEvent"/> published by Order.Api
/// via RabbitMQ. Uses Semantic Kernel / Ollama to generate a highly personalised
/// "Thank you for your purchase" email body that recommends complementary products,
/// then logs the result via Serilog / Seq. No HTTP request context is required —
/// this consumer runs as a fully autonomous background actor.
/// </summary>
public sealed class OrderPlacedEventConsumer : IConsumer<OrderPlacedEvent>
{
	private readonly IChatCompletionService _chatCompletion;
	private readonly Kernel _kernel;
	private readonly ILogger<OrderPlacedEventConsumer> _logger;

	public OrderPlacedEventConsumer(
		IChatCompletionService chatCompletion,
		Kernel kernel,
		ILogger<OrderPlacedEventConsumer> logger)
	{
		_chatCompletion = chatCompletion ?? throw new ArgumentNullException(nameof(chatCompletion));
		_kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
	{
		var evt = context.Message;

		_logger.LogInformation(
			"OrderPlacedEvent received. OrderId={OrderId} CustomerId={CustomerId} ItemCount={ItemCount} Timestamp={Timestamp}",
			evt.OrderId, evt.CustomerId, evt.Items.Count, evt.Timestamp);

		var prompt = BuildPrompt(evt);

#pragma warning disable SKEXP0070
		var settings = new OllamaPromptExecutionSettings { Temperature = 0.7f };
#pragma warning restore SKEXP0070

		string emailBody;
		try
		{
			var result = await _chatCompletion.GetChatMessageContentAsync(
				prompt,
				settings,
				_kernel,
				context.CancellationToken);

			emailBody = result.Content
				?? "Thank you for your order. We truly appreciate your business and look forward to serving you again!";
		}
		catch (Exception ex)
		{
			_logger.LogError(ex,
				"Semantic Kernel failed to generate thank-you email for OrderId={OrderId}",
				evt.OrderId);
			return;
		}

		_logger.LogInformation(
			"AI-generated thank-you email for OrderId={OrderId} CustomerId={CustomerId}. EmailBody={EmailBody}",
			evt.OrderId, evt.CustomerId, emailBody);
	}

	/// <summary>
	/// Constructs the single-turn prompt sent to Ollama. Provides the LLM with
	/// structural order metadata so it can produce a contextually relevant,
	/// tech-product-focused recommendation without requiring an additional
	/// gateway round-trip from the background consumer.
	/// </summary>
	private static string BuildPrompt(OrderPlacedEvent evt)
	{
		var itemLines = string.Join("\n",
			evt.Items.Select((item, i) =>
				$"  {i + 1}. Quantity: {item.Quantity}  |  VariantId: {item.VariantId}"));

		return $"""
			You are a warm, expert e-commerce email copywriter for TechItEz — a premium technology products store specialising in electronics, peripherals, and accessories.

			A customer has just successfully completed a purchase. Write a highly personalised, engaging "Thank you for your purchase" email body.

			STRICT RULES:
			- Output the email BODY only. Do not include a subject line, "Dear [name]" salutation, or any metadata headers.
			- Do NOT invent or fabricate specific product names you cannot know. Refer to the items as "your recent purchase" or "the tech items you selected".
			- Base your complementary product recommendations on the context of a technology/electronics order.
			- Recommend exactly 2–3 specific complementary product CATEGORIES (e.g. screen protectors, mechanical keyboards, USB-C hubs, surge protectors, cable management solutions). Be concrete and useful, not generic.
			- Tone: warm, professional, slightly enthusiastic. Brand voice: modern tech-savvy retailer.

			The email MUST contain these four parts in order:
			1. A genuine, enthusiastic opening thank-you acknowledging the order.
			2. A brief confirmation that Order #{evt.OrderId} ({evt.Items.Count} item(s)) is now being processed and will be on its way soon.
			3. A "You might also love" section with 2–3 specific complementary product category recommendations, each with a one-sentence benefit explanation.
			4. A warm, brand-consistent closing sign-off from the TechItEz team.

			Order context:
			- Order ID   : {evt.OrderId}
			- Placed at  : {evt.Timestamp:f}
			- Items      :
			{itemLines}
			""";
	}
}