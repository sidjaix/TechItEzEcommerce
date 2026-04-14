# Active Context: Current Work Focus

## Current Phase: Phase 8 — AI Observability

Phases 1–7 are 100% complete and stable. The `AI.Api` microservice is live with full Semantic Kernel auto function-calling, `ProductPlugin`, `CartPlugin`, and the MassTransit `OrderPlacedEventConsumer` reacting autonomously to RabbitMQ domain events.

The immediate active focus is **Phase 8: AI Observability** — adding structured, traceable observability specifically for all AI/agent operations inside `AI.Api` (token usage, tool-call spans, prompt/completion logging, latency metrics).

### Completed Milestones (Phase 7 — now closed)

1. **`ProductPlugin`** — `Shared/Api-Common/Plugins/ProductPlugin.cs` — Two `[KernelFunction]`s:
   - `search_products`: Calls `GET /api/catalog/semantic-search?query=...` via YARP. Returns curated JSON with `Name`, `Slug`, `StartingPrice`, `Category`, `Brand`, `Summary`.
   - `get_product_details`: Calls `GET /api/catalog/{slug}` via YARP. Returns curated JSON with `Name` and `Variants[]` each containing `VariantId`, `Price`, `StockQuantity`, `Attributes`.
   - Registered in `AddSemanticKernelWithOllama()` as `kernelBuilder.Plugins.AddFromType<ProductPlugin>("Product")`.
2. **Tool-chaining System Prompt** — `AI/AI.Api/Controllers/ChatController.cs` `SystemPrompt` updated with explicit mandatory 4-step add-to-cart protocol: `search_products` → `get_product_details` → variant selection → `add_item_to_cart`. Agent is explicitly forbidden from asking users for GUIDs.
3. **MassTransit AI Consumer** — `AI/AI.Api/Consumers/OrderPlacedEventConsumer.cs`:
   - `IConsumer<OrderPlacedEvent>` — fully autonomous, no HTTP request context.
   - Receives `OrderPlacedEvent` from RabbitMQ queue `ai-order-placed-event` (isolated via `KebabCaseEndpointNameFormatter("ai", false)`).
   - Invokes `IChatCompletionService` (Ollama) via Semantic Kernel to generate a personalised "Thank you for your purchase" email body with 2–3 complementary tech-product category recommendations.
   - Prompt constructed from `OrderId`, `CustomerId`, `Items`, `Timestamp` — no gateway round-trip needed.
   - Result logged as structured event to Serilog / Seq.
   - Registered in `AI/AI.Api/Program.cs` via `busConfig.AddConsumer<OrderPlacedEventConsumer>()`.

### Next Immediate Goals (Phase 8)

1. **AI-specific OpenTelemetry spans** — Capture Semantic Kernel `KernelFunction` invocations, tool-call decisions, and Ollama completion latency as OpenTelemetry spans visible in Jaeger.
2. **Token usage metrics** — Log prompt/completion token counts from `ChatMessageContent.Metadata` to Serilog/Seq per request and per consumer event.
3. **Prompt/completion audit log** — Structured Serilog events for every AI interaction (prompt, chosen functions, final reply) tagged with `TraceId` for correlation with the distributed trace.

## Recent Changes

- **NEW**: `AI/AI.Api/Consumers/OrderPlacedEventConsumer.cs` — `IConsumer<OrderPlacedEvent>` autonomous background actor. Receives `OrderPlacedEvent` from RabbitMQ, invokes Semantic Kernel / Ollama to generate personalised thank-you email body, logs result to Seq.
- **MODIFIED**: `AI/AI.Api/Program.cs` — Added `AddMassTransit` block (step 7): `KebabCaseEndpointNameFormatter("ai", false)`, `AddConsumer<OrderPlacedEventConsumer>()`, `UsingRabbitMq` with `cfg.ConfigureEndpoints(ctx)`.
- **NEW**: `Shared/Api-Common/Plugins/ProductPlugin.cs` — `ProductPlugin` with `search_products` and `get_product_details` KernelFunctions.
- **MODIFIED**: `Shared/Api-Common/Extensions/AiExtensions.cs` — Added `kernelBuilder.Plugins.AddFromType<ProductPlugin>("Product")`.
- **MODIFIED**: `AI/AI.Api/Controllers/ChatController.cs` — `SystemPrompt` expanded with explicit tool-chaining protocol and GUID rule.

## Active Decisions & Considerations

- `AI.Api` is the single dedicated AI brain — it owns all agentic logic (HTTP chat AND event-driven consumers). Domain microservices remain unaware of AI.
- `OrderPlacedEventConsumer` receives its own isolated copy of every `OrderPlacedEvent` via the `ai-order-placed-event` queue — it does NOT share a queue with `Cart.Api` or `Product.Api` consumers.
- `IChatCompletionService` and `Kernel` are constructor-injected into the consumer from the standard DI container — the same Ollama/SK setup used by `ChatController` is reused at zero extra cost.
- Consumer has no dependency on `IHttpContextAccessor` — it is a pure background actor and never touches the HTTP pipeline.
- All plugins (`CartPlugin`, `ProductPlugin`) live in `Shared/Api-Common/Plugins/` — reusable across any agent host, no circular project references.
- Agent HTTP calls route through YARP (`http://api_gateway:8080`) — gateway auth and rate-limiting policies apply identically to an AI agent and a human user.
- Bearer JWT is forwarded via `TokenDelegatingHandler`; the agent never manages user identity explicitly.
- `FunctionChoiceBehavior.Auto()` is set in `ChatController` — no additional configuration needed for auto-invocation of plugins.
- Configuration key `Gateway:BaseUrl` must be present in `appsettings.json` of any service calling `AddSemanticKernelWithOllama`. Docker default: `http://api_gateway:8080`.
