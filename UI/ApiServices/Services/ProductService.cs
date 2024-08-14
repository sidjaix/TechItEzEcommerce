using ApiServices.Models;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;
using Newtonsoft.Json;

namespace ApiServices.Services;

public class ProductService : IProductService
{
    private readonly IBaseService _baseService;
    public ProductService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateNewProductAsync(ProductModel productModel)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product",
            Data = productModel,
            ApiMethod = ApiMethod.POST,
            ContentType = ContentType.Json
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> UpdateExistingProductAsync(ProductModel productModel)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product/update",
            Data = productModel,
            ApiMethod = ApiMethod.PUT,
            ContentType = ContentType.Json
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<List<ProductModel>> GetProductsAsync()
    {
        var products = new List<ProductModel>();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product",
            ApiMethod = ApiMethod.GET
        };
        var response = await _baseService.SendAsync(request);
        if (response != null && response.IsSuccess)
        {
            products = JsonConvert.DeserializeObject<List<ProductModel>>(Convert.ToString(response.Result));
        }
        return products;
    }

    public async Task<ResponseDto> GetProductDetailAsync(int productId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product/{productId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> GetProductsByCategoryAsync(int categoriesId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product/GetProductByCategory/{categoriesId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }

    public async Task<ResponseDto> DeleteProductAsync(int productId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product/{productId}",
            ApiMethod = ApiMethod.DELETE
        };
        var response = await _baseService.SendAsync(request);
        return response;
    }
}
