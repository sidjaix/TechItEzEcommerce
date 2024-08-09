using Newtonsoft.Json;
using Product_Core.Entities;
using System.Net.Http.Json;
using System.Text;

namespace ApiServices.Product;

public class CategoryService : ICategoryService
{
    private readonly HttpClient httpClient;
    public CategoryService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Category> CreateNewCategoryAsync(Category productModel)
    {
        var url = $"category";
        var category = new Category();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(category);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<Category>(jsonString);
            return category;
        }
        return category;
    }

    public async Task<Category> UpdateExistingCategoryAsync(Category productModel)
    {
        var url = $"category/update";
        var category = new Category();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(category);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<Category>(jsonString);
            return category;
        }
        return category;
    }

    public async Task<List<Category>> GetAllCategoryAsync()
    {
        var url = "category";
        var categories = new List<Category>();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            categories = JsonConvert.DeserializeObject<List<Category>>(jsonString);
            return categories;
        }
        return categories;
    }

    public async Task<Category> GetCategoryAsync(int categoryId)
    {
        var url = $"category/{categoryId}";
        var category = new Category();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            category = JsonConvert.DeserializeObject<Category>(jsonString);
            return category;
        }
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var url = $"category/{categoryId}";
        var responce = await httpClient.DeleteAsync(url);
        return responce.IsSuccessStatusCode;
    }

}
