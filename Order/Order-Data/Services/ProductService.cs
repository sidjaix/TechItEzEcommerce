using Order_Data.Services.IServices;
using Order_Core.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Order_Data.Services;

public class ProductService(HttpClient httpClient) : IProductService
{
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        var products = new List<ProductModel>();
        var response = await httpClient.GetAsync("/api/product");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseMessage = JsonConvert.DeserializeObject<ResponseDto>(responseContent);
            products = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(responseMessage.Result));
        }
        return products;
    }

    public async Task<ProductModel> GetProductDetailAsync(int productId)
    {
        ProductModel product = new();
        var response = await httpClient.GetAsync($"/api/product/{productId}");
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<ProductModel>(responseContent);
        }
        return product;
    }

    public async Task<List<ProductModel>> GetProductsByIdsAsync(List<int> productIds)
    {
        var products = new List<ProductModel>();
        var response = await httpClient.PostAsJsonAsync($"/api/product/GetProductsByIds", productIds);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ProductModel>>(responseContent);
        }
        return products;
    }
}
