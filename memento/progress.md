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
