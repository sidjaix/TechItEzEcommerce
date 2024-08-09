using Product_Core.Models;

namespace Product_Data.Repositories;

public interface ICategoryRepository
{
    Task<CategoryModel> CreateNewCategoryAsync(CategoryModel categoryModel);
    Task<CategoryModel> UpdateExistingCategoryAsync(CategoryModel categoryModel);
    Task<List<CategoryModel>> GetAllCategoryAsync();
    Task<CategoryModel> GetCategoryAsync(int categoriesId);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
