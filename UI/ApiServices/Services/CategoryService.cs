using Newtonsoft.Json;
using ApiServices.Models;
using System.Net.Http.Json;
using System.Text;
using ApiServices.Services.IService;
using ApiServices.Utility.Enums;
using Microsoft.Extensions.Configuration;

namespace ApiServices.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient httpClient;
    private readonly IBaseService baseService;
    public CategoryService(HttpClient httpClient, IBaseService baseService, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        baseService.BaseAddress = configuration["ApiBaseAddress:Product"];
        this.baseService = baseService;
    }

    public async Task<ResponseDto> CreateNewCategoryAsync(CategoryModel categoryData)
    {
        var request = new RequestDto
        {
            Url = "/api/category",
            ApiMethod = ApiMethod.POST,
            Data = categoryData,
            ContentType = ContentType.Json
        };

        var response = await baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> UpdateExistingCategoryAsync(CategoryModel categoryData)
    {
        var request = new RequestDto
        {
            Url = $"/api/category/update",
            ApiMethod = ApiMethod.PUT,
            Data = categoryData,
            ContentType = ContentType.Json
        };
        var response = await baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> GetAllCategoryAsync()
    {
        var request = new RequestDto
        {
            Url = "api/category",
            ApiMethod = ApiMethod.GET
        };

        var response = await baseService.SendAsync(request);

        return response;
    }

    public async Task<ResponseDto> GetCategoryAsync(int categoryId)
    {
        var request = new RequestDto
        {
            Url = $"/api/category/{categoryId}",
            ApiMethod = ApiMethod.GET
        };

        var response = await baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> DeleteCategoryAsync(int categoryId)
    {
        var request = new RequestDto
        {
            Url = $"/api/category/{categoryId}",
            ApiMethod = ApiMethod.DELETE
        };
        var response = await baseService.SendAsync(request);
        return response;
    }

}
