using ApiServices.Models;
using ApiServices.Models.Product;

namespace ApiServices.Services.IService;

public interface IProductService
{
    Task<ResponseDto> CreateNewProductAsync(CreateProductViewModel productModel);
    Task<ResponseDto> UpdateExistingProductAsync(CreateProductViewModel productModel);
    Task<List<ProductViewModel>> GetProductsAsync();
    Task<ResponseDto> GetProductDetailAsync(int productId);
    Task<List<ProductViewModel>> GetProductsByCategoryAsync(int categoriesId);
    Task<ResponseDto> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
