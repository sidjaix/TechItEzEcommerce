using ApiServices.Models;
using ApiServices.Services.IService;
using ApiServices.Utility.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace ApiServices.Services;

public class ProductService : IProductService
{
    private readonly IBaseService _baseService;
    private readonly HttpClient httpClient;
    public ProductService(HttpClient httpClient, IBaseService baseService, IConfiguration configuration)
    {
        this.httpClient = httpClient;
        baseService.BaseAddress = configuration["ApiBaseAddress:Product"];
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateNewProductAsync(ProductModel productModel)
    {
        var request = new RequestDto
        {
            Url = "/api/product",
            Data = productModel,
            ApiMethod = ApiMethod.POST,
            ContentType = ContentType.Json,
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<ProductModel> UpdateExistingProductAsync(ProductModel productModel)
    {
        var url = $"product/update";
        var product = new ProductModel();
        // Convert the data to JSON
        var jsonData = JsonConvert.SerializeObject(product);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responce = await httpClient.PostAsJsonAsync(url, content);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<ProductModel>(jsonString);
            return product;
        }
        return product;
    }

    public async Task<List<ProductModel>> GetProductsAsync()
    {
        var url = "product";
        var products = new List<ProductModel>();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ProductModel>>(jsonString);
            return products;
        }
        return products;
    }

    public async Task<ProductModel> GetProductDetailAsync(int productId)
    {
        var url = $"product/{productId}";
        var product = new ProductModel();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            product = JsonConvert.DeserializeObject<ProductModel>(jsonString);
            return product;
        }
        return product;
    }

    public async Task<List<ProductModel>> GetProductsByCategoryAsync(int categoriesId)
    {
        var url = $"product/GetProductByCategory/{categoriesId}";
        var products = new List<ProductModel>();
        var responce = await httpClient.GetAsync(url);
        if (responce.IsSuccessStatusCode)
        {
            var jsonString = await responce.Content.ReadAsStringAsync();
            products = JsonConvert.DeserializeObject<List<ProductModel>>(jsonString);
            return products;
        }
        return products;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var url = $"product/{productId}";
        var responce = await httpClient.DeleteAsync(url);
        return responce.IsSuccessStatusCode;
    }
}
