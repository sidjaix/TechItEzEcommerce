using Product_Core.Entities;

namespace Product_Data.Repositories;

public interface ICategoryRepository
{
    Task<Category> CreateNewCategoryAsync(Category categoryModel);
    Task<Category> UpdateExistingCategoryAsync(Category categoryModel);
    Task<List<Category>> GetAllCategoryAsync();
    Task<Category> GetCategoryAsync(int categoriesId);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
