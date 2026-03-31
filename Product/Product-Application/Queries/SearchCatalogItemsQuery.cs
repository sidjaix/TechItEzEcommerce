using MediatR;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;
using System;

namespace ProductApplication.Queries;

public record SearchCatalogItemsQuery(string Term) : IRequest<IEnumerable<CatalogItemDto>>;

public class SearchCatalogItemsQueryHandler : IRequestHandler<SearchCatalogItemsQuery, IEnumerable<CatalogItemDto>>
{
    private readonly ICatalogRepository _repository;
    public SearchCatalogItemsQueryHandler(ICatalogRepository repo) => _repository = repo;

    public async Task<IEnumerable<CatalogItemDto>> Handle(SearchCatalogItemsQuery request, CancellationToken ct)
    {
        // For POC, we'll filter the active list. In production, this would be a dedicated SQL Search or ElasticSearch.
        var items = await _repository.GetActiveCatalogItemsAsync(ct);
        var filtered = items.Where(i => i.BaseName.Contains(request.Term, StringComparison.OrdinalIgnoreCase)
                                     || i.ShortSummary.Contains(request.Term, StringComparison.OrdinalIgnoreCase));

        return filtered.Select(item => new CatalogItemDto
        {
            Id = item.Id,
            Name = item.BaseName,
            Slug = item.Slug,
            CategoryName = item.Category?.Name ?? "General",
            StartingPrice = item.Variants.Any() ? item.Variants.Min(v => v.Price) : 0
        });
    }
}
