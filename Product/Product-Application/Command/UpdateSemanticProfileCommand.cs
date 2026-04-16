using MediatR;
using ProductApplication.Interfaces;

namespace ProductApplication.Command;

public record UpdateSemanticProfileCommand(Guid CatalogItemId, string ShortSummary, string SemanticDescription) : IRequest<bool>;

public class UpdateSemanticProfileCommandHandler(ICatalogRepository repository, ICatalogItemSemanticProfileUpdateEventPublisher catalogUpdatePublisher) : IRequestHandler<UpdateSemanticProfileCommand, bool>
{

	public async Task<bool> Handle(UpdateSemanticProfileCommand request, CancellationToken cancellationToken)
	{
		var item = await repository.GetByIdAsync(request.CatalogItemId, cancellationToken);
		if (item == null) return false;

		item.UpdateSemanticProfile(request.ShortSummary, request.SemanticDescription);

		await repository.UpdateAsync(item, cancellationToken);

		// Publish using the abstraction
		await catalogUpdatePublisher.PublishSemanticProfileUpdatedAsync(item.Id, item.BaseName, item.ShortSummary, item.SemanticDescription, cancellationToken);

		return true;
	}
}