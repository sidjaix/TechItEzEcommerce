using MediatR;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;

namespace ProductApplication.Queries;

public record SemanticSearchQuery(string Query, int Top = 5) : IRequest<IEnumerable<CatalogItemDto>>;

public class SemanticSearchQueryHandler : IRequestHandler<SemanticSearchQuery, IEnumerable<CatalogItemDto>>
{
	private readonly ISemanticSearchService _semanticSearchService;
	private readonly ICatalogRepository _repository;

	public SemanticSearchQueryHandler(
		ISemanticSearchService semanticSearchService,
		ICatalogRepository repository)
	{
		_semanticSearchService = semanticSearchService;
		_repository = repository;
	}

	public async Task<IEnumerable<CatalogItemDto>> Handle(SemanticSearchQuery request, CancellationToken ct)
	{
		// 1. Get ordered list of Product IDs based on semantic similarity
		var matchingProductIds = await _semanticSearchService.SearchProductIdsAsync(request.Query, request.Top, ct);

		if (!matchingProductIds.Any())
		{
			return Enumerable.Empty<CatalogItemDto>();
		}

		// 2. Resolve the full product entities from the database
		// We use the existing method which returns all active items, then filter.
		// In a real high-scale system, we'd add an `GetItemsByIdsAsync(IEnumerable<Guid>)` to the repo.
		var activeItems = await _repository.GetActiveCatalogItemsAsync(ct);

		// Ensure we preserve the exact ranking order returned by the vector search
		var resultList = new List<CatalogItemDto>();

		foreach (var id in matchingProductIds)
		{
			var item = activeItems.FirstOrDefault(i => i.Id == id);
			if (item != null)
			{
				resultList.Add(new CatalogItemDto
				{
					Id = item.Id,
					Name = item.BaseName,
					Slug = item.Slug,
					ShortSummary = item.ShortSummary ?? item.SemanticDescription,
					CategoryName = item.Category?.Name ?? "General",
					BrandName = item.Brand?.Name ?? "N/A",
					StartingPrice = item.Variants.Any() ? item.Variants.Min(v => v.Price) : 0
				});
			}
		}

		return resultList;
	}
}