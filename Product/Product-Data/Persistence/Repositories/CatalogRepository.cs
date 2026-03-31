using Microsoft.EntityFrameworkCore;
using ProductApplication.Interfaces;
using ProductCore.Entities;
using ProductData.Persistence;


namespace ProductData.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly ProductDbContext db;
    public CatalogRepository(ProductDbContext context)
    {
        db = context;
    }
    public async Task<IEnumerable<CatalogItem>> GetActiveCatalogItemsAsync(CancellationToken cancellationToken)
    {
        return await db.CatalogItems
            .AsNoTracking() // Performance boost for read-only queries
            .Include(c => c.Category) // Fetches CategoryName
            .Include(c => c.Brand)    // Fetches BrandName
            .Include(c => c.Variants) // Needed for StartingPrice
                .ThenInclude(v => v.Images.Where(i => i.IsPrimary)) // Only fetch primary images to save memory
            .Where(c => c.IsPublished)
            .ToListAsync(cancellationToken);
    }

    public async Task<CatalogItem?> GetBySlugWithDetailsAsync(string slug, CancellationToken cancellationToken)
    {
        return await db.CatalogItems
            .AsNoTracking()
            .Include(c => c.Category)
            .Include(c => c.Brand)
            .Include(c => c.Tags)
            .Include(c => c.Reviews)
            .Include(c => c.Variants)
                .ThenInclude(v => v.Images) // Fetch all images for the detail page
            .FirstOrDefaultAsync(c => c.Slug == slug, cancellationToken);
    }

    public async Task<CatalogItem> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // Tracking is enabled here because we use this for Updates/Commands
        return await db.CatalogItems
            .Include(c => c.Variants)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task AddAsync(CatalogItem item, CancellationToken cancellationToken)
    {
        await db.CatalogItems.AddAsync(item, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CatalogItem item, CancellationToken cancellationToken)
    {
        db.CatalogItems.Update(item);
        await db.SaveChangesAsync(cancellationToken);
    }
}
