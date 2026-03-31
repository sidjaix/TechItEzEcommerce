using ProductApplication.DTOs;
using ProductCore.Entities;

namespace ProductApplication.Interfaces;

public interface ITaxonomyRepository
{
    Task<IEnumerable<Category>> GetCategoryTreeAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Brand>> GetBrandsAsync(CancellationToken cancellationToken);
}
