using ApiServices.Models;
using ApiServices.Models.Product;
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

    public async Task<ResponseDto> CreateNewProductAsync(CreateProductViewModel productModel)
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

    public async Task<ResponseDto> UpdateExistingProductAsync(CreateProductViewModel productModel)
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

    public async Task<List<ProductViewModel>> GetProductsAsync()
    {
        var products = new List<ProductViewModel>();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product",
            ApiMethod = ApiMethod.GET
        };
        var response = await _baseService.SendAsync(request);
        if (response != null && response.IsSuccess)
        {
            products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));
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

    public async Task<List<ProductViewModel>> GetProductsByCategoryAsync(int categoriesId)
    {
        var products = new List<ProductViewModel>();
        var request = new RequestDto
        {
            Url = $"{ApplicationData.ProductApiBaseAddress}/api/product/GetProductByCategory/{categoriesId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await _baseService.SendAsync(request);
        if (response != null && response.IsSuccess)
        {
            products = JsonConvert.DeserializeObject<List<ProductViewModel>>(Convert.ToString(response.Result));
        }
        return products;
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
