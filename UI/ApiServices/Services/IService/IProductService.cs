using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface IProductService
{
    public Task<ResponseDto> CreateNewProductAsync(ProductModel productModel);
    public Task<ResponseDto> UpdateExistingProductAsync(ProductModel productModel);
    public Task<List<ProductModel>> GetProductsAsync();
    public Task<ResponseDto> GetProductDetailAsync(int productId);
    public Task<ResponseDto> GetProductsByCategoryAsync(int categoriesId);
    Task<ResponseDto> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
