using ApiServices.Models;
using ApiServices.Models.Product;

namespace ApiServices.Services.IService;

public interface ICategoryService
{
    public Task<ResponseDto> CreateNewCategoryAsync(CategoryViewModel categoryModel);
    public Task<ResponseDto> UpdateExistingCategoryAsync(CategoryViewModel categoryModel);
    public Task<List<CategoryViewModel>> GetAllCategoryAsync();
    public Task<ResponseDto> GetCategoryAsync(int productId);
    Task<ResponseDto> DeleteCategoryAsync(int productId);
    //Task<List<CategoryModel>> GetFeaturedCategories();
}
