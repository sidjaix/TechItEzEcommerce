using MediatR;
using ProductApplication.Interfaces;

namespace ProductApplication.Command;

public record AddVariantCommand(Guid CatalogItemId, string Sku, decimal Price, int stockQuantity, string AttributesJson) : IRequest<bool>;

public class AddVariantCommandHandler : IRequestHandler<AddVariantCommand, bool>
{
    private readonly ICatalogRepository _repository;
    public AddVariantCommandHandler(ICatalogRepository repository) { _repository = repository; }

    public async Task<bool> Handle(AddVariantCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.CatalogItemId, cancellationToken);
        if (item == null) return false;

        // Uses the domain method to ensure invariants (e.g., duplicate SKU checks)
        item.AddVariant(request.Sku, request.Price, request.stockQuantity, request.AttributesJson);

        await _repository.UpdateAsync(item, cancellationToken);
        return true;
    }
}
