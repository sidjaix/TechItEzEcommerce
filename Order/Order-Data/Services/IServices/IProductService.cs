using Order_Core.Models;

namespace Order_Data.Services.IServices;

public interface IProductService
{
    Task<List<ProductModel>> GetProductsAsync();
    Task<List<ProductModel>> GetProductsByIdsAsync(List<int> productIds);
    Task<ProductModel> GetProductDetailAsync(int productId);
}
