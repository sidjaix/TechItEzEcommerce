# Product Context: User Personas & UI/UX Goals

## User Personas

1. **The Shopper (Human):** Browses products, manages a cart, and securely checks out. Expects fast response times and accurate inventory reflection.
2. **The Autonomous Agent (AI):** A system actor operating via Semantic Kernel. It requires deterministic API responses, clear error codes (RFC 7807), and standard JWT authentication to autonomously query products and execute orders on behalf of users.
3. **The System Architect (Human):** Monitors the health of the system. Relies heavily on Jaeger (Traces) and Seq (Logs) to debug failed transactions and monitor AI token usage/latency.

## UI/UX & API Experience Goals

- **Deterministic API Contracts:** AI agents struggle with inconsistent JSON. Responses must strictly adhere to shared DTO contracts.
- **Traceable Failures:** When an action fails, the UX (whether a React frontend or an AI Agent prompt) must receive a `TraceId` to pinpoint the exact microservice failure.
- **Semantic Discovery:** Transitioning from traditional keyword search to vector-based semantic search, allowing users (and agents) to find products using natural language intent.
