# AI Infrastructure (Local Models & Memory)

## Docker Setup (The Kickoff)

I have successfully updated the `docker-compose.yml` to support local AI infrastructure for Phase 6 without modifying any existing C# code.
Here is a summary of the new additions and how your future .NET APIs will communicate with them:

### 1. Qdrant (Vector Database)

- **Purpose:** To store and search high-dimensional vectors (embeddings) generated from your domain entities (like `CatalogItem`).
- **Ports Exchanged:**
  - `6333`: REST API (useful for testing or viewing via a UI)
  - `6334`: gRPC endpoint (used by high-performance SDKs)
- **Future Communication:** Your background worker or `Product.Api` will use the official `.NET Qdrant SDK`. It connects via gRPC for high performance. The services are on the same bridge network (`techitez_network`), so the connection string inside your APIs will simply be `http://qdrant:6334` or `http://qdrant:6333` depending on the protocol used.

### 2. Ollama (Local LLM Host)

- **Purpose:** To host models like Llama 3 or Phi-3 completely locally to provide Generative AI text and embeddings.
- **Port Exchanged:**
  - `11434`: HTTP API endpoint.
- **Future Communication:** Using **Microsoft Semantic Kernel**, you can configure it to chat with or generate embeddings from Ollama directly. Your backend APIs will issue standard HTTP requests internally to `http://ollama:11434`.

Both of these services use named volumes (`qdrant-data` and `ollama-data`) to ensure your pulled LLM models and stored vectors persist across container restarts.
You can now start these new infrastructure containers anytime using `docker-compose up -d`.

### Semantic Kernel Integration

I have successfully introduced Microsoft Semantic Kernel into the shared library (`Api-Common`) while respecting the architectural constraints.

Here's a breakdown of the modifications made:

1. **Installed Dependencies**: I used the .NET CLI to install `Microsoft.SemanticKernel`, `Microsoft.SemanticKernel.Connectors.Ollama`, and `Microsoft.SemanticKernel.Connectors.Qdrant` into the `Shared/Api-Common/Api-Common.csproj` file. This centralizes the AI orchestration SDK so that all microservices have consistent access to it without duplicating package references.
2. **Created Extension Method**: I created a new file at `Shared/Api-Common/Extensions/AiExtensions.cs`. This file contains the `AddSemanticKernelWithOllama()` extension method. It registers Semantic Kernel with the `IServiceCollection` and specifically configures it to use Ollama for chat completions, seamlessly pointing to the local Docker URL (`http://ollama:11434`) exposed by your `docker-compose.yml`.
3. **Updated Memento**: I updated both `memento/activeContext.md` and `memento/progress.md` to reflect that the Semantic Kernel integration has been successfully bootstrapped as part of "Phase 6" AI readiness.

Because everything was contained within `Api-Common`, no domain-specific APIs (User, Product, Cart, Order) were altered. They are now prepared to utilize Semantic Kernel via DI whenever you're ready to proceed with the next steps!

## RAG (Retrieval-Augmented Generation)

I have successfully implemented the requested `ProductVectorizationWorker` inside the `Product.Api` project, adhering to your Clean Architecture and EF Core constraints.

### What I did

1. **Semantic Kernel Integration (`AiExtensions.cs`)**:
   - Added Semantic Kernel and Qdrant connector registrations into the shared `Api-Common` extension `AddSemanticKernelWithOllama`.
   - Setup `AddQdrantVectorStore` mapped to the local Qdrant container endpoint.
   - Configured the Ollama text embedding generator model pointing to `all-minilm`.

2. **Vector Model (`ProductRecord.cs`)**:
   - Created the `ProductRecord` class (already referenced) that uses Microsoft's `Microsoft.Extensions.VectorData` abstractions to define properties like `VectorStoreRecordKey` and `VectorStoreRecordVector(384)`.

3. **Background Service (`ProductVectorizationWorker.cs`)**:
   - Created `ProductVectorizationWorker` inheriting from `BackgroundService`.
   - Uses `IServiceProvider` to scope dependencies properly since `ProductDbContext` is scoped.
   - Fetches all published products (`p.IsPublished == true`) using `ProductDbContext` natively.
   - Computes Semantic Kernel vectors for the product using the injected `IEmbeddingGenerator<string, Embedding<float>>`.
   - Upserts these vector mappings along with metadata (Name, Description) into Qdrant using the `IVectorStore` generic abstraction.

4. **Worker Registration**:
   - Registered `ProductVectorizationWorker` inside `Product.Api/Program.cs` using `builder.Services.AddHostedService<ProductApi.Services.ProductVectorizationWorker>()`.

The `Product-Api` project builds successfully and the semantic background worker will process catalog data into Qdrant automatically on service startup!

I have successfully implemented the semantic search functionality adhering strictly to Clean Architecture and DDD principles, fulfilling all constraints.

The solution consists of the following components:

1. **Contract Definition (`Product-Application`)**: Added `ISemanticSearchService` which acts as the boundary. The Application layer orchestrates the lookup but knows nothing about AI models or Vector Databases.
2. **CQRS Orchestration (`Product-Application`)**: Created `SemanticSearchQuery` and `SemanticSearchQueryHandler` which accept a natural language text query, request an ordered list of matched product IDs from the injected search service, and hydrate those exact IDs into the standard `CatalogItemDto`.
3. **AI Integration (`Product-Api`)**: Created `SemanticSearchService` taking advantage of Microsoft Semantic Kernel (`IEmbeddingGenerator`) and Qdrant (`VectorStoreCollection`). It converts the user text intent into vectors and computes cosine similarity against the `ProductRecord` vector store.
4. **Service Registration (`Product-Api`)**: Bootstrapped the necessary dependencies in `Program.cs` including the shared `AddSemanticKernelWithOllama()` extension.
5. **Endpoint Exposure (`Product-Api`)**: Added the exact requested endpoint: `GET /api/products/semantic-search?query={text}` to `CatalogController`.

The API can now fulfill semantic searches and return matching products mapped perfectly to the pre-existing DTO contracts, ready for both standard UI consumption and Agentic AI operations.

## `CartPlugin` — Semantic Kernel Tool Implementation Complete

### Files Created / Modified

| File                                                                                           | Operation                                                                |
| ---------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------ |
| [`Shared/Api-Common/Plugins/CartPlugin.cs`](Shared/Api-Common/Plugins/CartPlugin.cs)           | **Created** (new `Plugins/` folder inside existing `Api-Common` project) |
| [`Shared/Api-Common/Extensions/AiExtensions.cs`](Shared/Api-Common/Extensions/AiExtensions.cs) | **Modified** — 3 new registrations added                                 |

---

### What Was Built

#### [`CartPlugin.cs`](Shared/Api-Common/Plugins/CartPlugin.cs)

- `sealed class CartPlugin` in namespace `ApiCommon.Plugins` with file-scoped namespace and constructor injection of `IHttpClientFactory`
- Private `record AddItemRequest(Guid VariantId, string ProductName, decimal UnitPrice, int Quantity)` — a local mirror of `CartController.AddItemRequest` that keeps `Api-Common` free of any `Cart-Application` project reference
- One `[KernelFunction("add_item_to_cart")]` method — [`AddItemToCartAsync()`](Shared/Api-Common/Plugins/CartPlugin.cs:52):
  - **Method-level `[Description]`**: Instructs the LLM exactly when to use this tool, what pre-conditions are required (valid VariantId from catalog, authenticated context), the upsert behavior on duplicate adds, and what it returns
  - **Per-parameter `[Description]`** on all four parameters (`variantId`, `productName`, `unitPrice`, `quantity`): Each description tells the LLM the data source, format, defaults, and constraints
  - Calls `POST /api/cart/AddItem` via `_httpClientFactory.CreateClient("GatewayClient")` — routes through YARP gateway, not directly to `cart_api`
  - Guards against `quantity <= 0` with an immediate descriptive return string
  - Catches `HttpRequestException` network failures and surfaces them as readable strings (not exceptions)
  - Returns structured success (`"Successfully added 2x 'Trail Shoe' (VariantId: ..., Unit Price: £129.99) to the cart."`) or failure with HTTP status code and response body

#### [`AiExtensions.cs`](Shared/Api-Common/Extensions/AiExtensions.cs) — Delta (steps 5, 6, 7)

```csharp
// 5. TokenDelegatingHandler — reads Bearer JWT from IHttpContextAccessor, forwards on outgoing requests
services.AddTransient<TokenDelegatingHandler>();

// 6. Named "GatewayClient" HttpClient — base address from Gateway:BaseUrl config, JWT auto-forwarded
services.AddHttpClient("GatewayClient", client =>
{
    client.BaseAddress = new Uri(configuration["Gateway:BaseUrl"] ?? "http://api_gateway:80");
})
.AddHttpMessageHandler<TokenDelegatingHandler>();

// 7. CartPlugin imported into the Kernel — DI-resolved, IHttpClientFactory injected automatically
kernelBuilder.Plugins.AddFromType<CartPlugin>("Cart");
```

---

### Architecture of the Call Chain

```mermaid
flowchart TD
    AgentHost
    Kernel
    CartPlugin
    IHttpClientFactory
    GatewayClient
    TokenDelegatingHandler
    YARP
    CartApi

    AgentHost --> Kernel
    Kernel --> CartPlugin
    CartPlugin --> IHttpClientFactory
    IHttpClientFactory --> GatewayClient
    GatewayClient --> TokenDelegatingHandler
    TokenDelegatingHandler --> YARP
    YARP --> CartApi
```

---

### Configuration Required

Any service host calling `AddSemanticKernelWithOllama()` must have this key in `appsettings.json`:

```json
{
  "Gateway": {
    "BaseUrl": "http://api_gateway:80"
  }
}
```

The Docker-internal default `http://api_gateway:80` is already hardcoded as the fallback, so no configuration change is strictly required in the existing Docker Compose setup.

---

### Zero Breaking Changes

- No new `.csproj` or `.sln` changes — `CartPlugin` lives inside the existing `Api-Common` class library
- No changes to `CartController.cs`, `AddItemToCartCommand.cs`, or any domain layer
- `Microsoft.SemanticKernel` `1.74.0` and `Microsoft.Extensions.VectorData` (2026 API with `[VectorStoreKey]`/`[VectorStoreData]`/`[VectorStoreVector]`) were already present in [`Api-Common.csproj`](Shared/Api-Common/Api-Common.csproj)
- The existing [`TokenDelegatingHandler`](Shared/Api-Common/Handlers/TokenDelegatingHandler.cs) was reused without modification

## `AI.Api` — Central AI Brain — Implementation Complete ✅

### Files Created

| File                                                                                   | Description                                                                                                  |
| -------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| [`AI/AI.Api/AI.Api.csproj`](AI/AI.Api/AI.Api.csproj)                                   | Minimal Web API project — only dependency is `Api-Common`                                                    |
| [`AI/AI.Api/Program.cs`](AI/AI.Api/Program.cs)                                         | Full service bootstrap: Serilog → OTEL → Auth → Swagger → `AddSemanticKernelWithOllama` → health/controllers |
| [`AI/AI.Api/Controllers/ChatController.cs`](AI/AI.Api/Controllers/ChatController.cs)   | `POST /api/chat` — SK function-calling loop with `FunctionChoiceBehavior.Auto()`                             |
| [`AI/AI.Api/appsettings.json`](AI/AI.Api/appsettings.json)                             | Production config: Docker-internal URLs for Ollama, Qdrant, Gateway, Seq, Jaeger                             |
| [`AI/AI.Api/appsettings.Development.json`](AI/AI.Api/appsettings.Development.json)     | Localhost overrides for all infra services                                                                   |
| [`AI/AI.Api/Properties/launchSettings.json`](AI/AI.Api/Properties/launchSettings.json) | Dev port `5005`                                                                                              |
| [`AI/Dockerfile`](AI/Dockerfile)                                                       | Multi-stage build, non-root `appuser`, `/health` healthcheck                                                 |

### Files Modified

| File                                                                                 | Change                                                                                         |
| ------------------------------------------------------------------------------------ | ---------------------------------------------------------------------------------------------- |
| [`docker-compose.yml`](docker-compose.yml)                                           | Added `ai_api` service on port `5005:80`, depends on `api_gateway` + `ollama`                  |
| [`ApiGateway/appsettings.json`](ApiGateway/appsettings.json)                         | Added `ai-api-route` (`/api/ai/{**}`) + `ai-swagger-route` + `ai-cluster` → `http://ai_api:80` |
| [`ApiGateway/appsettings.Development.json`](ApiGateway/appsettings.Development.json) | Added `ai-cluster` dev override → `http://localhost:5005`                                      |
| [`ApiGateway/Program.cs`](ApiGateway/Program.cs)                                     | Added `/ai-docs/v1/swagger.json` to master Swagger dashboard                                   |
| [`TechItEzEcommerce.sln`](TechItEzEcommerce.sln)                                     | Added `AI` solution folder and `AI.Api` project registration                                   |

---

### How It Works

When an authenticated user sends `POST /api/chat` with `{ "prompt": "Add the cheapest mechanical keyboard to my cart" }`:

1. [`ChatController.Chat()`](AI/AI.Api/Controllers/ChatController.cs:60) builds a `ChatHistory` with a scoped system prompt and the user's message.
2. [`OllamaPromptExecutionSettings`](AI/AI.Api/Controllers/ChatController.cs:83) with `FunctionChoiceBehavior.Auto()` is passed to [`IChatCompletionService.GetChatMessageContentAsync()`](AI/AI.Api/Controllers/ChatController.cs:91).
3. Semantic Kernel runs the **auto function-calling loop** — the Ollama LLM decides to call `Cart.add_item_to_cart`; SK invokes [`CartPlugin.AddItemToCartAsync()`](Shared/Api-Common/Plugins/CartPlugin.cs:52) automatically.
4. `CartPlugin` uses the `GatewayClient` HttpClient (with [`TokenDelegatingHandler`](Shared/Api-Common/Handlers/TokenDelegatingHandler.cs) forwarding the caller's JWT) to `POST /api/cart/AddItem` through YARP.
5. Once the tool result feeds back, the LLM generates a final confirmation message which is returned as `{ "reply": "..." }`.

## Tool-Chaining Implementation Complete

All three files have been created/modified. Here is a precise summary of every change.

---

### 1. CREATED — [`Shared/Api-Common/Plugins/ProductPlugin.cs`](Shared/Api-Common/Plugins/ProductPlugin.cs)

A new `sealed class ProductPlugin` following the exact same pattern as [`CartPlugin`](Shared/Api-Common/Plugins/CartPlugin.cs) — DI-injected `IHttpClientFactory`, `"GatewayClient"` named client, YARP routing with JWT forwarding.

Two `[KernelFunction]`s:

| SK Function Name                                                        | HTTP Call                                    | Purpose in Chain                                                            |
| ----------------------------------------------------------------------- | -------------------------------------------- | --------------------------------------------------------------------------- |
| [`search_products`](Shared/Api-Common/Plugins/ProductPlugin.cs:42)      | `GET /api/catalog/semantic-search?query=...` | Step 1 — natural-language search, returns `Name` + `Slug` + `StartingPrice` |
| [`get_product_details`](Shared/Api-Common/Plugins/ProductPlugin.cs:115) | `GET /api/catalog/{slug}`                    | Step 2 — resolves exact `VariantId`, `Price`, `Attributes` per variant      |

**Key design decisions:**

- Uses `System.Text.Json.JsonDocument` to parse and re-serialize a curated response subset — `Api-Common` has **zero** dependency on `ProductApplication`
- Each function's `[Description]` explicitly names its role in the chain (`"Call this FIRST"` / `"Call this SECOND"`) so the LLM understands sequencing even without the system prompt
- Returns actionable error strings on failure (network error, 404, empty results) consistent with `CartPlugin` convention

---

### 2. MODIFIED — [`Shared/Api-Common/Extensions/AiExtensions.cs`](Shared/Api-Common/Extensions/AiExtensions.cs:72)

Added one registration at step 8, after the existing `CartPlugin` line:

```csharp
kernelBuilder.Plugins.AddFromType<ProductPlugin>("Product");
```

`ProductPlugin` shares the already-registered `"GatewayClient"` HttpClient and `TokenDelegatingHandler` — zero additional infrastructure cost. Both `AI.Api` and `Product.Api` (which also calls `AddSemanticKernelWithOllama`) will now have both plugins loaded in their kernels.

---

### 3. MODIFIED — [`AI/AI.Api/Controllers/ChatController.cs`](AI/AI.Api/Controllers/ChatController.cs:30) — `SystemPrompt`

Replaced the old Rule 2 ("tell the user you need to look it up") with an explicit **mandatory 4-step protocol**:

- **Step 1** — Call `Product-search_products` with the user's natural-language description
- **Step 2** — Call `Product-get_product_details` with the `Slug` from step 1 to get exact `VariantId` and `Price`
- **Step 3** — Select the correct variant (ask human-readable clarification if ambiguous — never ask for a GUID)
- **Step 4** — Call `Cart-add_item_to_cart` with resolved `VariantId`, `ProductName`, `Price`, and `Quantity` before replying

Added an explicit rule: _"NEVER ask the user for a VariantId, ProductId, or any GUID — resolve them autonomously."_

> `FunctionChoiceBehavior.Auto()` at [`ChatController.cs:97`](AI/AI.Api/Controllers/ChatController.cs:97) was already set correctly and is unchanged — this IS the Ollama-connector equivalent of `ToolCallBehavior.AutoInvokeKernelFunctions`.

---

### Tool-Chain Data Flow

```mermaid
flowchart LR
    A["User: add running shoes to cart"]
    B["Product-search_products\nGET /api/catalog/semantic-search"]
    C["Product-get_product_details\nGET /api/catalog/{slug}"]
    D["Cart-add_item_to_cart\nPOST /api/cart/AddItem"]
    E["Agent: Added 'Trail Runner XT200'\nSize 10, £129.99 to your cart"]

    A --> B
    B --> C
    C --> D
    D --> E
```

The agent now resolves `VariantId` and `UnitPrice` autonomously through two product catalog hops before invoking the cart — the user is never asked for a technical identifier.

All 6 changes are implemented and verified — `dotnet build` exits with **Build succeeded, 0 errors, 0 warnings**.

---

### What Was Built

### New File: [`AI/AI.Api/Consumers/OrderPlacedEventConsumer.cs`](AI/AI.Api/Consumers/OrderPlacedEventConsumer.cs)

A fully autonomous MassTransit v8 consumer that:

- Implements `IConsumer<OrderPlacedEvent>` (MassTransit v8 contract, no deprecated `IConsumer` overloads).
- Injects `IChatCompletionService`, `Kernel`, and `ILogger<T>` — no `IHttpContextAccessor` or JWT context needed (background actor).
- Builds a structured prompt from the `OrderPlacedEvent` payload (`OrderId`, `CustomerId`, `Timestamp`, `Items[]`) instructing Ollama to produce a 4-part personalized thank-you email with complementary tech product category recommendations.
- Logs the complete generated email body using **Serilog structured logging** (`LogInformation` with `{OrderId}`, `{CustomerId}`, `{EmailBody}` properties — routed to Seq).
- Handles `IChatCompletionService` failures gracefully with `LogError` — the bus message is not re-queued on AI failures.

---

### Modified Files

| File                                                                                  | Change                                                                                                                                            |
| ------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------- |
| [`AI/AI.Api/AI.Api.csproj`](AI/AI.Api/AI.Api.csproj:12)                               | Added `MassTransit.RabbitMQ` v8.2.5 `<PackageReference>`                                                                                          |
| [`AI/AI.Api/Program.cs`](AI/AI.Api/Program.cs:1)                                      | Added `using MassTransit` + `AddMassTransit` block (section 7) with `KebabCaseEndpointNameFormatter("ai", false)` → queue `ai-order-placed-event` |
| [`AI/AI.Api/appsettings.json`](AI/AI.Api/appsettings.json:22)                         | Added `"RabbitMq": { "Host": "amqp://guest:guest@rabbit_mq:5672" }`                                                                               |
| [`AI/AI.Api/appsettings.Development.json`](AI/AI.Api/appsettings.Development.json:22) | Added `"RabbitMq": { "Host": "amqp://guest:guest@localhost:5672" }`                                                                               |
| [`docker-compose.yml`](docker-compose.yml:148)                                        | Added `rabbit_mq` to `depends_on` and `RabbitMq__Host=${RabbitMq_Host}` to `environment` of the `ai_api` service                                  |

---

### Queue Isolation

The `KebabCaseEndpointNameFormatter("ai", false)` produces the exclusive queue name **`ai-order-placed-event`**. This is completely isolated from `cart-order-placed-event` and `product-order-placed-event` — each service gets its own copy of every `OrderPlacedEvent` with no competing consumers.

### Architecture Compliance

- **MassTransit v8.2.5** — `AddMassTransit`, `UsingRabbitMq`, `ConfigureEndpoints(ctx)` — no deprecated v7 APIs.
- **Clean Architecture** — Consumer lives in the `AI.Api` (API layer). No MassTransit references in Application or Domain layers.
- **Serilog structured logging** — all properties are named contextual keys, not interpolated strings.
- **No sensitive data logged** — no PII, no raw JWT tokens.
- **W3C Trace Context** — MassTransit OpenTelemetry instrumentation (via the existing `AddStandardOpenTelemetry`) automatically propagates trace context through the RabbitMQ message headers.
