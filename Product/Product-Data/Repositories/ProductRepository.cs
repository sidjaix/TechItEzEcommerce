using Microsoft.EntityFrameworkCore;
using Product_Core;
using Product_Core.Entities;
using Product_Core.Mapper;
using Product_Core.Models;

namespace Product_Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext db;
    public ProductRepository(ProductDbContext context)
    {
        db = context;
    }

    public async Task<ProductModel> CreateNewProductAsync(ProductModel productData)
    {
        Product product = productData.MapToEntity();
        product.ProductImages.Add(new ProductImage
        {
            ImageUrl = productData.ProductImageUrl
        });
        db.Products.Add(product);
        await db.SaveChangesAsync();
        var productDto = product.MapToDto();
        return productDto;
    }
    public async Task<ProductModel> UpdateExistingProductAsync(ProductModel productData)
    {

        var existingProduct = await db.Products.FindAsync(productData.ProductId);
        if (existingProduct == null)
        {
            return default;
        }
        existingProduct.ProductName = productData.ProductName;
        existingProduct.CategoryId = productData.CategoryId;
        existingProduct.Description = productData.Description;
        existingProduct.Price = productData.Price;
        existingProduct.LastModifiedBy = 1;

        db.Attach(existingProduct);
        await db.SaveChangesAsync();
        return existingProduct.MapToDto();
    }
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        var products = await db.Products
        .Include(p => p.ProductImages)
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            CategoryId = x.CategoryId,
            ProductName = x.ProductName,
            Description = x.Description,
            Price = x.Price,
            ProductImageUrl = x.ProductImages.FirstOrDefault(pi => pi.ProductId == x.ProductId).ImageUrl
        })
        .AsSplitQuery()
        .ToListAsync();
        return products;
    }
    public async Task<ProductModel> GetProductDetailAsync(int productId)
    {
        var product = await db.Products
        .Include(p => p.ProductImages)
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            CategoryId = x.CategoryId,
            ProductName = x.ProductName,
            Description = x.Description,
            Price = x.Price,
            ProductImageUrl = x.ProductImages.FirstOrDefault(pi => pi.ProductId == x.ProductId).ImageUrl
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
        .Include(p => p.ProductImages)
        .Where(x => x.CategoryId == categoryId)
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            CategoryId = x.CategoryId,
            ProductName = x.ProductName,
            Description = x.Description,
            Price = x.Price,
            ProductImageUrl = x.ProductImages.FirstOrDefault(pi => pi.ProductId == x.ProductId).ImageUrl
        })
        .AsSplitQuery()
        .ToListAsync();

        return products;
    }
    public async Task<bool> DeleteProductAsync(int productId)
    {
        var numberOfRowDeleted = await db.Products.Where(p => p.ProductId == productId).ExecuteDeleteAsync();
        return numberOfRowDeleted > 0;
    }
}