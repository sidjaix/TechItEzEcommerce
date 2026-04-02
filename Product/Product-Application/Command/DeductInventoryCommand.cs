using MediatR;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;

namespace ProductApplication.Command;

public record DeductInventoryCommand(List<DeductInventoryItemDto> Items) : IRequest<bool>;

// 2. The Handler
public class DeductInventoryCommandHandler : IRequestHandler<DeductInventoryCommand, bool>
{
    private readonly ICatalogRepository _repository;

    public DeductInventoryCommandHandler(ICatalogRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeductInventoryCommand request, CancellationToken ct)
    {
        if (request.Items == null || request.Items.Count == 0)
        {
            return false;
        }

        foreach (var requestedItem in request.Items)
        {
            // 1. Find the parent CatalogItem that owns this specific VariantId
            var catalogItem = await _repository.GetByVariantIdAsync(requestedItem.VariantId, ct);

            if (catalogItem != null)
            {
                // 2. Find the exact variant in the loaded collection
                var variant = catalogItem.Variants.FirstOrDefault(v => v.Id == requestedItem.VariantId);

                if (variant != null)
                {
                    // 3. Execute the encapsulated domain logic (throws exception if stock goes below 0)
                    variant.DecreaseStock(requestedItem.Quantity);

                    // 4. Save the updated aggregate back to the database
                    await _repository.UpdateAsync(catalogItem, ct);
                }
            }
        }

        return true;
    }
}
