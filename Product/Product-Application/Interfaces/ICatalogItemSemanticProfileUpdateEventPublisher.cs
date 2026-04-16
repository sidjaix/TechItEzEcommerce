using System;

namespace ProductApplication.Interfaces;

public interface ICatalogItemSemanticProfileUpdateEventPublisher
{
	Task PublishSemanticProfileUpdatedAsync(Guid Id, string baseName, string shortSummary, string semanticDescription, CancellationToken ct);
}
