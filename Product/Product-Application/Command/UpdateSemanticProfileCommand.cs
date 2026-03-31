using MediatR;
using ProductApplication.Interfaces;
using System;

namespace ProductApplication.Command;

public record UpdateSemanticProfileCommand(Guid CatalogItemId, string ShortSummary, string SemanticDescription) : IRequest<bool>;

public class UpdateSemanticProfileCommandHandler : IRequestHandler<UpdateSemanticProfileCommand, bool>
{
    private readonly ICatalogRepository _repository;
    public UpdateSemanticProfileCommandHandler(ICatalogRepository repository) { _repository = repository; }

    public async Task<bool> Handle(UpdateSemanticProfileCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.CatalogItemId, cancellationToken);
        if (item == null) return false;

        item.UpdateSemanticProfile(request.ShortSummary, request.SemanticDescription);

        // When this saves, your Outbox interceptor (built in Phase 5) will catch it and push to Vector DB
        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}