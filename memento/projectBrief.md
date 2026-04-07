# Project Brief: Core Requirements

## Overview

`tie-e-commerce` is a production-grade, event-driven microservices platform built on .NET 8. While it functions as a complete e-commerce system (Identity, Catalog, Cart, Order), its primary strategic purpose is to serve as the foundational execution environment ("the body") for advanced Generative AI, RAG (Retrieval-Augmented Generation), and Agentic AI experiments.

## Core Requirements

1. **Strict Architectural Integrity:** The system must maintain uncompromised Domain-Driven Design (DDD) and Clean Architecture boundaries. The Application layer must never depend on external infrastructure or message brokers.
2. **Event-Driven Resilience:** Core business processes (like checkout) must utilize eventual consistency via RabbitMQ to ensure high availability and decoupled domain logic.
3. **Enterprise Observability:** Every interaction must be fully traceable using OpenTelemetry (W3C Trace Context) and structured logging to ensure AI agents and human operators can debug complex distributed flows.
4. **AI-Ready APIs:** All endpoints must be predictable, stateless, and documented (Swagger/OpenAPI) to allow seamless "Function Calling" by Microsoft Semantic Kernel and local LLMs (Ollama).
5. **Secure by Default:** Exception details must never leak. All unhandled errors must be formatted as RFC 7807 ProblemDetails containing actionable Trace IDs.
