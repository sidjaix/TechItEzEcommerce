using ApiServices.Models;
using ApiServices.Models.Product;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;
using Newtonsoft.Json;

namespace ApiServices.Services;

public class CategoryService : ICategoryService
{
    private readonly IBaseService _baseService;
    public CategoryService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateNewCategoryAsync(CategoryViewModel categoryData)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/category",
            ApiMethod = ApiMethod.POST,
            Data = categoryData,
            ContentType = ContentType.Json
        };

        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> UpdateExistingCategoryAsync(CategoryViewModel categoryData)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/category/update",
            ApiMethod = ApiMethod.PUT,
            Data = categoryData,
            ContentType = ContentType.Json
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
    {
        var categories = new List<CategoryViewModel>();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/category",
            ApiMethod = ApiMethod.GET
        };

        var response = await _baseService.SendAsync(request);
        if (response != null && response.IsSuccess)
        {
            categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(Convert.ToString(response.Result));
        }
        return categories;
    }

    public async Task<ResponseDto> GetCategoryAsync(int categoryId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/category/{categoryId}",
            ApiMethod = ApiMethod.GET
        };

        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> DeleteCategoryAsync(int categoryId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/category/{categoryId}",
            ApiMethod = ApiMethod.DELETE
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

}
