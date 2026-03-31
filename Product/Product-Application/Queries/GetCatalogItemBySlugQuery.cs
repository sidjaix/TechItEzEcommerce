using MediatR;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;

namespace ProductApplication.Queries;

public record GetCatalogItemBySlugQuery(string Slug) : IRequest<CatalogItemDetailDto?>;

public class GetCatalogItemBySlugQueryHandler(ICatalogRepository repository) : IRequestHandler<GetCatalogItemBySlugQuery, CatalogItemDetailDto?>
{
    public async Task<CatalogItemDetailDto?> Handle(GetCatalogItemBySlugQuery request, CancellationToken cancellationToken)
    {
        var item = await repository.GetBySlugWithDetailsAsync(request.Slug, cancellationToken);
        if (item == null) return null;

        return new CatalogItemDetailDto
        {
            Id = item.Id,
            Name = item.BaseName,
            Slug = item.Slug,
            ShortSummary = item.ShortSummary,
            SemanticDescription = item.SemanticDescription,
            CategoryName = item.Category?.Name,
            BrandName = item.Brand?.Name,
            StartingPrice = item.Variants.Any() ? item.Variants.Min(v => v.Price) : 0,

            Tags = item.Tags.Select(t => t.Name).ToList(),

            Variants = item.Variants.Select(v => new VariantDto
            {
                Id = v.Id,
                Sku = v.Sku,
                Price = v.Price,
                StockQuantity = v.StockQuantity,
                AttributesJson = v.AttributesJson,
                Images = v.Images.Select(i => new ImageDto
                {
                    ImageUrl = i.ImageUrl,
                    AltText = i.AltText,
                    IsPrimary = i.IsPrimary
                }).ToList()
            }).ToList(),

            Reviews = item.Reviews.Select(r => new ReviewDto
            {
                ReviewerAlias = r.ReviewerAlias,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CreatedAt = r.CreatedAt
            }).ToList()
        };
    }
}
