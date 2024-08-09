namespace ApiServices.Product;

public interface IProductService
{
    public Task<Product_Core.Entities.Product> CreateNewProductAsync(Product_Core.Entities.Product productModel);
    public Task<Product_Core.Entities.Product> UpdateExistingProductAsync(Product_Core.Entities.Product productModel);
    public Task<List<Product_Core.Entities.Product>> GetProductsAsync();
    public Task<Product_Core.Entities.Product> GetProductDetailAsync(int productId);
    public Task<List<Product_Core.Entities.Product>> GetProductsByCategoryAsync(int categoriesId);
    Task<bool> DeleteProductAsync(int productId);
    //Task<List<Product>> GetFeaturedProducts();
}
