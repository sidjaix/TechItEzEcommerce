# Technology Context: The Stack

## Backend Services

- **Framework:** .NET 8 (C# 12)
- **Gateway:** YARP (Microsoft.ReverseProxy)
- **Mediation:** MediatR
- **Message Bus:** MassTransit v8.2.5
- **Data Access:** Entity Framework Core 8

## Data & Infrastructure (Docker)

- **Relational DB:** Azure SQL Edge (mssql)
- **Message Broker:** RabbitMQ (3-management)
- **Logging Dashboard:** Seq (Datalust)
- **Tracing Dashboard:** Jaeger (All-in-one, OTLP enabled)

## Observability Packages (`Api-Common`)

- `Serilog.AspNetCore`, `Serilog.Sinks.Seq`
- `OpenTelemetry.Extensions.Hosting`
- `OpenTelemetry.Instrumentation.AspNetCore`
- `OpenTelemetry.Instrumentation.Http`
- `OpenTelemetry.Instrumentation.SqlClient` (Stable replacement for EF Core beta)
- `OpenTelemetry.Exporter.OpenTelemetryProtocol`

## Upcoming AI Stack (Phase 6)

- **AI Orchestration:** Microsoft Semantic Kernel
- **Local LLM Hosting:** Ollama (Llama 3 / Phi-3)
- **Vector Database:** Qdrant or Redisearch
