using ProductCore.Entities;

namespace ProductApplication.Interfaces;

public interface ICatalogRepository
{
    // Queries
    Task<IEnumerable<CatalogItem>> GetActiveCatalogItemsAsync(CancellationToken cancellationToken);
    Task<CatalogItem> GetBySlugWithDetailsAsync(string slug, CancellationToken cancellationToken);
    Task<CatalogItem> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<CatalogItem> GetByVariantIdAsync(Guid variantId, CancellationToken cancellationToken);

    // Commands
    Task AddAsync(CatalogItem item, CancellationToken cancellationToken);
    Task UpdateAsync(CatalogItem item, CancellationToken cancellationToken);
}
