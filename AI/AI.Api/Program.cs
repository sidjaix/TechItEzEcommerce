using AiApi.Consumers;
using ApiCommon.Contracts;
using ApiCommon.Extensions;
using ApiCommon.Handlers;
using ApiCommon.Options;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var appName = "AI-Api";

// ==========================================
// 1. CONFIGURATION & LOGGING
// ==========================================
builder.Host.AddStandardSerilog(appName);
builder.Services.AddStandardOpenTelemetry(config, appName);

// ==========================================
// 2. CONTROLLERS & JSON FORMATTING
// ==========================================
builder.Services.AddControllers()
	.AddNewtonsoftJson();

// ==========================================
// 3. CORS POLICY
// ==========================================
builder.Services.AddCorsPolicy();

// ==========================================
// 4. AUTHENTICATION & AUTHORIZATION
// ==========================================
// JWT validation ensures the Bearer token is present in the HTTP context
// so that TokenDelegatingHandler can forward it to downstream microservices.
builder.Services.Configure<JwtOptions>(config.GetSection("JWT"));
builder.AddAppAuthentication();

// ==========================================
// 5. SWAGGER / OPENAPI
// ==========================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(appName);

// ==========================================
// 6. AI / SEMANTIC KERNEL
// ==========================================
// IHttpContextAccessor is required by TokenDelegatingHandler which is registered
// inside AddSemanticKernelWithOllama. It must be registered before calling that method.
builder.Services.AddHttpContextAccessor();

// Registers the Kernel, Ollama chat completion, Qdrant vector store,
// the GatewayClient HttpClient with TokenDelegatingHandler, and CartPlugin.
builder.Services.AddSemanticKernelWithOllama(config);

// ==========================================
// 7. MESSAGE BUS (MassTransit / RabbitMQ)
// ==========================================
builder.Services.AddMassTransit(busConfig =>
{
	// Produces queue name: ai-order-placed-event
	// Isolated from cart-order-placed-event and product-order-placed-event
	busConfig.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("ai", false));

	busConfig.AddConsumer<OrderPlacedEventConsumer>();

	busConfig.UsingRabbitMq((ctx, cfg) =>
	{
		var host = config["RabbitMq:Host"] ?? "amqp://guest:guest@rabbit_mq:5672";

		cfg.Host(host);
		cfg.ConfigureEndpoints(ctx);
	});
});

// ==========================================
// 8. INFRASTRUCTURE
// ==========================================
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddHealthChecks();

var app = builder.Build();

// ==========================================
// 8. HTTP REQUEST PIPELINE (Strict Ordering)
// ==========================================

// 1. Error Handling
app.UseExceptionHandler();

// 2. Swagger
app.UseSwaggerWUIWithAuth(appName);

// 3. Routing
app.UseRouting();

// 4. CORS
app.UseCors("default");

// 5. Authentication & Request Tracking
app.UseAuthentication();
app.UseCustomContextTracking();

// 6. Authorization
app.UseAuthorization();

// 7. Map Endpoints
app.MapHealthChecks("/health");
app.MapControllers();

app.Run();