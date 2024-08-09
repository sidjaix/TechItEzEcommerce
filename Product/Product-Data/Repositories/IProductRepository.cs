using Product_Core.Entities;

namespace Product_Data.Repositories;

public interface IProductRepository
{
    Task<Product> CreateNewProductAsync(Product productModel);
    Task<Product> UpdateExistingProductAsync(Product productModel);
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductDetailAsync(int productId);
    Task<List<Product>> GetProductsByCategoryAsync(int categoriesId);

    //Task<List<Product>> GetFeaturedProducts();
    Task DeleteProductAsync(int productId);
}
