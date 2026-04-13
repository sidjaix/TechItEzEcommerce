# Progress: What is Working

## ✅ Phase 1: Core API & Gateway

- YARP API Gateway routes traffic successfully to all backend microservices via Docker internal DNS.
- Domain, Application, Infrastructure, and API layers are strictly separated.

## ✅ Phase 2: Identity & Security

- `User.Api` issues valid JWT tokens.
- APIs validate tokens. `UserContextMiddleware` extracts Identity (`UserId`) for logging.

## ✅ Phase 3: Synchronous Domains

- `Cart.Api` successfully persists basket state.
- `Product.Api` successfully handles catalog queries.
- EF Core code-first migrations are applied and operational on Azure SQL Edge.

## ✅ Phase 4: Event-Driven Checkout

- `Order.Api` saves orders and publishes `OrderPlacedEvent` to RabbitMQ Exchange.
- MassTransit successfully routes copies of the event to prefixed queues.
- `Cart.Api` consumer clears the user's basket upon checkout.
- `Product.Api` consumer safely executes `DecreaseStock()` domain logic.

## ✅ Phase 5: Observability & Resilience

- Serilog streams enriched, structured logs to Seq natively.
- OpenTelemetry tracks W3C Trace Contexts across YARP, HTTP, SQL Server, and RabbitMQ, visualized as waterfalls in Jaeger.
- .NET 8 Global Exception Handler intercepts errors and returns standardized RFC 7807 ProblemDetails to the client.

## ✅ Phase 6: AI Transition (RAG & Agentic AI) - Complete

- Installed `Microsoft.SemanticKernel` 1.74.0 and connectors for Ollama and Qdrant into `Api-Common`.
- Created `AddSemanticKernelWithOllama()` extension method in `Api-Common` to inject Semantic Kernel, Ollama chat completion, and Qdrant vector store into DI.
- Configured `ProductVectorizationWorker` (Qdrant background worker) to generate and ingest text embeddings at startup via local Ollama.
- Implemented `GET /api/products/semantic-search` endpoint backed by `ISemanticSearchService` / Qdrant similarity search, returning `CatalogItemDto` results.
- **Semantic Kernel Plugins folder established** (`Shared/Api-Common/Plugins/`) — the canonical location for all AI-callable tool wrappers within the `Api-Common` shared library.
- **`CartPlugin` implemented** (`Shared/Api-Common/Plugins/CartPlugin.cs`):
  - Exposes `add_item_to_cart` as a `[KernelFunction]` with rich `[Description]` metadata for LLM function-calling.
  - Calls `POST /api/cart/AddItem` via the YARP API Gateway using `IHttpClientFactory` (named client `"GatewayClient"`).
  - Bearer JWT is forwarded automatically via `TokenDelegatingHandler` — agent acts as the authenticated user with no manual session management.
  - Registered in `AddSemanticKernelWithOllama()` via `kernelBuilder.Plugins.AddFromType<CartPlugin>("Cart")`.
- **`AI.Api` microservice created** (`AI/AI.Api/`) — dedicated central AI brain microservice:
  - `POST /api/chat` endpoint in `ChatController` accepts natural-language prompts from authenticated users.
  - Runs the Semantic Kernel **auto function-calling loop** (`FunctionChoiceBehavior.Auto()`) via `OllamaPromptExecutionSettings` — LLM autonomously decides which tools to invoke and feeds results back until a final reply is produced.
  - Fully bootstrapped: Serilog, OpenTelemetry, JWT auth, Swagger, `AddSemanticKernelWithOllama`, `GlobalExceptionHandler`, and health checks.
  - Multi-stage `Dockerfile` with non-root `appuser` and `/health` healthcheck.
  - Registered in `docker-compose.yml` on port `5005:80`, depends on `api_gateway` + `ollama`.
  - YARP routes added: `/api/ai/{**}` and `/ai-docs/v1/swagger.json` → `http://ai_api:80`.
  - `TechItEzEcommerce.sln` updated with the `AI` solution folder and `AI.Api` project.
- **Agent function-calling verified** — the agent successfully executes `CartPlugin.add_item_to_cart` autonomously in response to natural-language shopping prompts.

## 🚀 Phase 7: Autonomous Event-Driven Agents via RabbitMQ — In Progress

### ✅ Completed

- **`ProductPlugin`** (`Shared/Api-Common/Plugins/ProductPlugin.cs`):
  - `search_products` KernelFunction: `GET /api/catalog/semantic-search?query=...` via YARP. Returns curated JSON (Name, Slug, StartingPrice, Category, Brand, Summary).
  - `get_product_details` KernelFunction: `GET /api/catalog/{slug}` via YARP. Returns curated JSON with full Variants list (VariantId, Price, StockQuantity, Attributes).
  - Registered in `AddSemanticKernelWithOllama()` alongside `CartPlugin` — no `AI.Api` changes needed.
- **Tool-chaining System Prompt**: `ChatController.SystemPrompt` updated with mandatory 4-step add-to-cart protocol. Agent explicitly forbidden from asking users for GUIDs.

### 🔲 Remaining

- `OrderPlugin` — wrap order placement endpoint as a KernelFunction for autonomous checkout.
- Event-driven agent triggers — subscribe to RabbitMQ domain events and autonomously react (e.g., low-stock notification, order-confirmation follow-up) without a human prompt.
