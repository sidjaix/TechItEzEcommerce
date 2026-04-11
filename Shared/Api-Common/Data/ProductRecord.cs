using Microsoft.Extensions.VectorData;

namespace ApiCommon.Data;

public class ProductRecord
{
	[VectorStoreKey]
	public Guid Id { get; set; } // Qdrant prefers ulong or Guid for IDs

	[VectorStoreData]
	public string Name { get; set; }

	[VectorStoreData]
	public string Description { get; set; }

	[VectorStoreVector(Dimensions: 768)] // Ensure dimensions match your Ollama model
	public ReadOnlyMemory<float> Vector { get; set; }
}