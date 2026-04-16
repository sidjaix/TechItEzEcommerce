namespace ApiCommon.Contracts;

public record class CatalogItemSemanticProfileUpdatedEvent
{
	public Guid ProductId { get; init; }
	public string BaseName { get; init; }
	public string ShortSummary { get; init; }
	public string SemanticDescription { get; init; }
}