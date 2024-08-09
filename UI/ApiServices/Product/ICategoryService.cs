using Product_Core.Entities;

namespace ApiServices.Product;

public interface ICategoryService
{
    public Task<Category> CreateNewCategoryAsync(Category categoryModel);
    public Task<Category> UpdateExistingCategoryAsync(Category categoryModel);
    public Task<List<Category>> GetAllCategoryAsync();
    public Task<Category> GetCategoryAsync(int productId);
    Task<bool> DeleteCategoryAsync(int productId);
    //Task<List<Category>> GetFeaturedCategories();
}
