using ProductApplication.DTOs;

namespace ProductApplication.Interfaces;

public interface ICategoryRepository
{
    Task<CategoryDto> CreateNewCategoryAsync(CategoryDto categoryModel);
    Task<CategoryDto> UpdateExistingCategoryAsync(CategoryDto categoryModel);
    Task<List<CategoryDto>> GetAllCategoryAsync();
    Task<CategoryDto> GetCategoryAsync(int categoriesId);
    Task<bool> DeleteCategoryAsync(int categoryId);
}
