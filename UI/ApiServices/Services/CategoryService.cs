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
    private readonly IConfiguration configuration;
    public CategoryService(HttpClient httpClient, IBaseService baseService, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        baseService.BaseAddress = configuration["ApiBaseAddress:Product"];
        this.baseService = baseService;
    }

    public async Task<CategoryModel> CreateNewCategoryAsync(CategoryModel productModel)
    {
        var url = $"api/category";
        var category = new CategoryModel();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(category);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<CategoryModel>(jsonString);
            return category;
        }
        return category;
    }

    public async Task<CategoryModel> UpdateExistingCategoryAsync(CategoryModel productModel)
    {
        var url = $"api/category/update";
        var category = new CategoryModel();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(category);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<CategoryModel>(jsonString);
            return category;
        }
        return category;
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

    public async Task<CategoryModel> GetCategoryAsync(int categoryId)
    {
        var url = $"api/category/{categoryId}";
        var category = new CategoryModel();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<CategoryModel>(jsonString);
            return category;
        }
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var url = $"api/category/{categoryId}";
        var responce = await httpClient.DeleteAsync(url);
        return responce.IsSuccessStatusCode;
    }

}
