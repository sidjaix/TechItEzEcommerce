# Active Context: Current Work Focus

## Current Phase: Phase 7 — Autonomous Event-Driven Agents via RabbitMQ

Phases 1–6 are 100% complete and stable. The `AI.Api` microservice is live, the Semantic Kernel auto function-calling loop is verified working, and the agent successfully executes real cart operations in response to natural-language prompts.

The immediate active focus is **Phase 7: Autonomous Event-Driven Agents via RabbitMQ** — extending the agent beyond request/response into a fully autonomous, event-driven actor that reacts to domain events without requiring a human prompt.

### Completed Milestones (Phase 6 — now closed)

1. **Vectorization Pipeline:** Background worker ingests `CatalogItem` embeddings into Qdrant. `GET /api/products/semantic-search` is live.
2. **Local AI Infrastructure:** `docker-compose.yml` hosts Ollama and Qdrant.
3. **Semantic Kernel Integration:** `Microsoft.SemanticKernel` 1.74.0 SDK configured with Ollama/Qdrant in `Api-Common` via `AddSemanticKernelWithOllama()`.
4. **`CartPlugin`:** `Shared/Api-Common/Plugins/CartPlugin.cs` — `add_item_to_cart` KernelFunction calling `POST /api/cart/AddItem` via YARP with JWT forwarding via `TokenDelegatingHandler`.
5. **`AI.Api` microservice:** `POST /api/chat` (`ChatController`) — SK auto function-calling loop with `FunctionChoiceBehavior.Auto()`. Registered in `docker-compose.yml`, YARP, and solution. **Agent function-calling is verified working end-to-end.**

### Completed Milestones (Phase 7 — partial)

1. **`ProductPlugin`** — `Shared/Api-Common/Plugins/ProductPlugin.cs` — Two `[KernelFunction]`s:
   - `search_products`: Calls `GET /api/catalog/semantic-search?query=...` via YARP. Returns curated JSON with `Name`, `Slug`, `StartingPrice`, `Category`, `Brand`, `Summary`.
   - `get_product_details`: Calls `GET /api/catalog/{slug}` via YARP. Returns curated JSON with `Name` and `Variants[]` each containing `VariantId`, `Price`, `StockQuantity`, `Attributes`.
   - Registered in `AddSemanticKernelWithOllama()` as `kernelBuilder.Plugins.AddFromType<ProductPlugin>("Product")`.
2. **Tool-chaining System Prompt** — `AI/AI.Api/Controllers/ChatController.cs` `SystemPrompt` updated with explicit mandatory 4-step add-to-cart protocol: `search_products` → `get_product_details` → variant selection → `add_item_to_cart`. Agent is explicitly forbidden from asking users for GUIDs.

### Next Immediate Goals (Phase 7)

1. **`OrderPlugin`** — Wrap the order placement endpoint as a `[KernelFunction]` for autonomous checkout.
2. **Event-driven agent triggers** — Subscribe to RabbitMQ domain events (e.g., `OrderPlacedEvent`, low-stock events) and have the agent autonomously react without a human-initiated HTTP request.

## Recent Changes

- **NEW**: `Shared/Api-Common/Plugins/ProductPlugin.cs` — `ProductPlugin` with `search_products` and `get_product_details` KernelFunctions. Both call through YARP `"GatewayClient"` with JWT forwarding. Use `System.Text.Json.JsonDocument` for response parsing — no `ProductApplication` project reference.
- **MODIFIED**: `Shared/Api-Common/Extensions/AiExtensions.cs` — Added `kernelBuilder.Plugins.AddFromType<ProductPlugin>("Product")` at step 8.
- **MODIFIED**: `AI/AI.Api/Controllers/ChatController.cs` — `SystemPrompt` expanded with explicit tool-chaining protocol (4-step add-to-cart sequence) and explicit rule forbidding the agent from asking users for GUIDs.
- **NEW**: `AI/AI.Api/` — Full microservice: `AI.Api.csproj`, `Program.cs`, `Controllers/ChatController.cs`, `appsettings.json`, `appsettings.Development.json`, `Properties/launchSettings.json`, `AI/Dockerfile`.
- **MODIFIED**: `docker-compose.yml` — Added `ai_api` service (port `5005:80`).
- **MODIFIED**: `ApiGateway/appsettings.json` + `appsettings.Development.json` — Added `ai-api-route`, `ai-swagger-route`, `ai-cluster`.
- **MODIFIED**: `ApiGateway/Program.cs` — Added `/ai-docs/v1/swagger.json` to master Swagger dashboard.
- **MODIFIED**: `TechItEzEcommerce.sln` — Added `AI` solution folder and `AI.Api` project.

## Active Decisions & Considerations

- `AI.Api` is the single dedicated AI brain — it owns all agentic logic. Domain microservices (Cart, Product, Order) remain unaware of AI and are called through YARP as regular HTTP clients.
- All plugins (`CartPlugin`, `ProductPlugin`, future `OrderPlugin`) live in `Shared/Api-Common/Plugins/` — reusable across any agent host, no circular project references.
- Agent calls route through YARP (`http://api_gateway:8080`) — gateway auth and rate-limiting policies apply identically to an AI agent as to a human user.
- Bearer JWT is forwarded via `TokenDelegatingHandler`; the agent never manages user identity explicitly.
- `ProductPlugin` uses `System.Text.Json.JsonDocument` to parse gateway responses and re-serializes a curated subset — `Api-Common` has zero dependency on `ProductApplication`.
- The tool-chain is: `Product-search_products` → `Product-get_product_details` → `Cart-add_item_to_cart`. This is enforced in both `[Description]` metadata on each function AND in the system prompt.
- `FunctionChoiceBehavior.Auto()` (Ollama connector equivalent of `ToolCallBehavior.AutoInvokeKernelFunctions`) is already set in `ChatController` — no additional configuration needed for auto-invocation.
- Configuration key `Gateway:BaseUrl` must be present in `appsettings.json` of any service calling `AddSemanticKernelWithOllama`. Docker default: `http://api_gateway:8080`.
