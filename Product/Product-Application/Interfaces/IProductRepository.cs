using ProductApplication.DTOs;

namespace ProductApplication.Interfaces;

public interface IProductRepository
{
    Task<ProductDto> CreateNewProductAsync(ProductDto productDto);
    Task<ProductDto> UpdateExistingProductAsync(ProductDto productDto);
    Task<List<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductDetailAsync(int productId);
    Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId);
    Task<bool> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
