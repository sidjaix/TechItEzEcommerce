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

## 🚀 Phase 6: AI Transition (RAG & Agentic AI) - In Progress

- Installed `Microsoft.SemanticKernel` and connectors for Ollama and Qdrant into `Api-Common`.
- Created an extension method `AddSemanticKernelWithOllama()` in `Api-Common` to inject Semantic Kernel into dependency injection, configured to communicate with the local `ollama` Docker container.
- Configured a Qdrant background worker (`ProductVectorizationWorker`) to iterate over the active catalog, generate text embeddings via local Ollama, and perform vector ingestion automatically at startup.
- Implemented a `GET /api/products/semantic-search` endpoint orchestrating Qdrant similarity searches mapped cleanly to the `CatalogItemDto` interface, adhering strictly to Clean Architecture separation via `ISemanticSearchService`.
