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
