using MediatR;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;

namespace ProductApplication.Queries;

public record GetActiveCatalogItemsQuery : IRequest<IEnumerable<CatalogItemDto>>;

public class GetActiveCatalogItemsQueryHandler(ICatalogRepository catalogRepository) : IRequestHandler<GetActiveCatalogItemsQuery, IEnumerable<CatalogItemDto>>
{
    public async Task<IEnumerable<CatalogItemDto>> Handle(GetActiveCatalogItemsQuery request, CancellationToken cancellationToken)
    {
        var items = await catalogRepository.GetActiveCatalogItemsAsync(cancellationToken);

        // Map the Entity to the DTO (You can use AutoMapper here, but manual mapping is often faster and cleaner for simple lists)
        return items.Select(item => new CatalogItemDto
        {
            Id = item.Id,
            Name = item.BaseName,
            Slug = item.Slug,
            ShortSummary = item.ShortSummary,

            // Assuming your repository includes these navigation properties
            // If they are null, we fall back to "Unknown" safely
            CategoryName = item.Category?.Name ?? "Unknown",
            BrandName = item.Brand?.Name ?? "Unknown",

            // Grab the lowest price from the available variants
            StartingPrice = item.Variants.Count != 0 ? item.Variants.Min(v => v.Price) : 0
        });
    }
}
