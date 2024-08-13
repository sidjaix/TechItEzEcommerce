using Product_Core.Entities;
using Product_Core.Models;

namespace Product_Data.Repositories;

public interface IProductRepository
{
    Task<ProductModel> CreateNewProductAsync(ProductModel productModel);
    Task<ProductModel> UpdateExistingProductAsync(ProductModel productModel);
    Task<List<ProductModel>> GetProductsAsync();
    Task<ProductModel> GetProductDetailAsync(int productId);
    Task<List<ProductModel>> GetProductsByCategoryAsync(int categoriesId);
    Task<bool> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
