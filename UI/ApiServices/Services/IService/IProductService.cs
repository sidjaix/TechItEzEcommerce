using ApiServices.Models;
using ApiServices.Models.Product;

namespace ApiServices.Services.IService;

public interface IProductService
{
    public Task<ResponseDto> CreateNewProductAsync(CreateProductViewModel productModel);
    public Task<ResponseDto> UpdateExistingProductAsync(CreateProductViewModel productModel);
    public Task<List<ProductViewModel>> GetProductsAsync();
    public Task<ResponseDto> GetProductDetailAsync(int productId);
    public Task<ResponseDto> GetProductsByCategoryAsync(int categoriesId);
    Task<ResponseDto> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
