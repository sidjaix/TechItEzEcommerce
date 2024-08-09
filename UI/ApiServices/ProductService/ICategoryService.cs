using ApiServices.Models;

namespace ApiServices.ProductService;

public interface ICategoryService
{
    public Task<CategoryModel> CreateNewCategoryAsync(CategoryModel categoryModel);
    public Task<CategoryModel> UpdateExistingCategoryAsync(CategoryModel categoryModel);
    public Task<List<CategoryModel>> GetAllCategoryAsync();
    public Task<CategoryModel> GetCategoryAsync(int productId);
    Task<bool> DeleteCategoryAsync(int productId);
    //Task<List<CategoryModel>> GetFeaturedCategories();
}
