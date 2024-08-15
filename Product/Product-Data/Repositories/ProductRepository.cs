using Microsoft.EntityFrameworkCore;
using Product_Core;
using Product_Core.Entities;
using Product_Core.Mapper;
using Product_Core.Models;

namespace Product_Data.Repositories;

public class ProductRepository(ProductDbContext db) : IProductRepository
{
    public async Task<ProductModel> CreateNewProductAsync(ProductModel productData)
    {
        Product product = productData.MapToEntity();
        await db.Products.AddAsync(product);
        await db.SaveChangesAsync();
        var productDto = product.MapToDto();
        return productDto;
    }
    public async Task<ProductModel> UpdateExistingProductAsync(ProductModel productData)
    {
        var existingProduct = await db.Products
        .FindAsync(productData.ProductId);

        if (existingProduct is null)
        {
            return default;
        }
        existingProduct.ProductName = productData.ProductName;
        existingProduct.CategoryId = productData.CategoryId;
        existingProduct.Description = productData.Description;
        existingProduct.Price = productData.Price;
        existingProduct.ImageUrl = productData.ImageUrl;

        db.Attach(existingProduct);
        await db.SaveChangesAsync();
        return existingProduct.MapToDto();
    }
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        var products = await db.Products
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            CategoryId = x.CategoryId,
            ProductName = x.ProductName,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Price = x.Price,
        })
        .AsSplitQuery()
        .ToListAsync();
        return products;
    }
    public async Task<ProductModel> GetProductDetailAsync(int productId)
    {
        var product = await db.Products
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            CategoryId = x.CategoryId,
            ProductName = x.ProductName,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Price = x.Price
        })
        .AsSplitQuery()
        .SingleOrDefaultAsync(p => p.ProductId == productId);
        if (product == null)
        {
            return default;
        }
        return product;
    }
    public async Task<List<ProductModel>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await db.Products
        .Where(x => x.CategoryId == categoryId)
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            CategoryId = x.CategoryId,
            ProductName = x.ProductName,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
            Price = x.Price
        })
        .AsSplitQuery()
        .ToListAsync();

        return products;
    }
    public async Task<bool> DeleteProductAsync(int productId)
    {
        var numberOfRowDeleted = await db.Products
        .Where(p => p.ProductId == productId)
        .ExecuteDeleteAsync();
        return numberOfRowDeleted > 0;
    }
}