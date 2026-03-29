using Microsoft.EntityFrameworkCore;
using ProductApplication.Interfaces;
using ProductApplication.Mappers;
using ProductApplication.DTOs;
using ProductCore.Entities;
using ProductData.Persistence;

namespace ProductData.Repositories;

public class ProductRepository(ProductDbContext db) : IProductRepository
{
    public Task<ProductDto> CreateNewProductAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> GetProductDetailAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductDto>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductDto>> GetProductsByCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductDto> UpdateExistingProductAsync(ProductDto productDto)
    {
        throw new NotImplementedException();
    }
}