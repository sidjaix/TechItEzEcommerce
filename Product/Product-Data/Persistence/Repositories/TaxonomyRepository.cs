using Microsoft.EntityFrameworkCore;
using ProductApplication.Interfaces;
using ProductCore.Entities;
using ProductData.Persistence;

namespace ProductData.Repositories;

public class TaxonomyRepository(ProductDbContext db) : ITaxonomyRepository
{

    public async Task<IEnumerable<Category>> GetCategoryTreeAsync(CancellationToken cancellationToken)
    {
        // Fetch only top-level categories, EF Core will auto-wire the included SubCategories
        return await db.Categories
            .AsNoTracking()
            .Where(c => c.ParentCategoryId == null)
            .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.SubCategories) // 3 levels deep
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Brand>> GetBrandsAsync(CancellationToken cancellationToken)
    {
        return await db.Brands
        .AsNoTracking()
        .ToListAsync(cancellationToken);
    }
}