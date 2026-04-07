# System Patterns: Architecture Decisions

## 1. Clean Architecture & DDD

- **Domain Centric:** Logic lives in Aggregate Roots (e.g., `ProductVariant.DecreaseStock()`).
- **Anti-Corruption Layers:** Application layer commands/queries do not know about MassTransit or HTTP. Handlers use abstract interfaces implemented in the API/Infrastructure layers.
- **CQRS:** Read and write operations are strictly separated using MediatR.

## 2. API Gateway Pattern

- All external traffic routes through YARP (Yet Another Reverse Proxy). Internal microservices are not exposed to the host network directly.

## 3. Event-Driven Messaging (Pub/Sub)

- **Broker:** RabbitMQ managed by MassTransit v8.
- **Events:** Shared contracts living in `Api-Common`. Named in past tense (e.g., `OrderPlacedEvent`).
- **Queues:** Strict use of `KebabCaseEndpointNameFormatter` with microservice prefixes (e.g., `product-order-placed-event`) to prevent competing consumer dead-letters.

## 4. Observability Pipeline

- **Correlation:** Native .NET `Activity` class utilizing W3C Trace Context (`TraceId`). No custom correlation ID headers.
- **Logging:** Serilog pushing structured JSON to Seq.
- **Tracing:** OpenTelemetry exporting Spans (HTTP, SQL, MassTransit) to Jaeger via OTLP gRPC.

## 5. Global Exception Handling

- Utilizing .NET 8 `IExceptionHandler`. Unhandled exceptions automatically yield an RFC 7807 `ProblemDetails` JSON response injecting the current `traceId`.
