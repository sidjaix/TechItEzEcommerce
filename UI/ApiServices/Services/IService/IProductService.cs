using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface IProductService
{
    public Task<ResponseDto> CreateNewProductAsync(ProductModel productModel);
    public Task<ProductModel> UpdateExistingProductAsync(ProductModel productModel);
    public Task<List<ProductModel>> GetProductsAsync();
    public Task<ProductModel> GetProductDetailAsync(int productId);
    public Task<List<ProductModel>> GetProductsByCategoryAsync(int categoriesId);
    Task<bool> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
