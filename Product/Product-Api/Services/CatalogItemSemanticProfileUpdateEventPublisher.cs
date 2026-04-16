using ApiCommon.Contracts;
using MassTransit;
using ProductApplication.Interfaces;

namespace ProductApi.Services;

public class CatalogItemSemanticProfileUpdateEventPublisher(IPublishEndpoint publishEndpoint) : ICatalogItemSemanticProfileUpdateEventPublisher
{
	public async Task PublishSemanticProfileUpdatedAsync(Guid id, string baseName, string shortSummary, string semanticDescription, CancellationToken ct)
	{
		// Map Application DTO to the Shared API-Common Contract
		var eventPayload = new CatalogItemSemanticProfileUpdatedEvent
		{
			ProductId = id,
			BaseName = baseName,
			ShortSummary = shortSummary,
			SemanticDescription = semanticDescription
		};

		await publishEndpoint.Publish(eventPayload, ct);
	}
}
