using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface ICategoryService
{
    public Task<ResponseDto> CreateNewCategoryAsync(CategoryModel categoryModel);
    public Task<ResponseDto> UpdateExistingCategoryAsync(CategoryModel categoryModel);
    public Task<ResponseDto> GetAllCategoryAsync();
    public Task<ResponseDto> GetCategoryAsync(int productId);
    Task<ResponseDto> DeleteCategoryAsync(int productId);
    //Task<List<CategoryModel>> GetFeaturedCategories();
}
