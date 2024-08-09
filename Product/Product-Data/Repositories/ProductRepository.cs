using Microsoft.EntityFrameworkCore;
using Product_Core;
using Product_Core.Entities;

namespace Product_Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext db;
    public ProductRepository(ProductDbContext context)
    {
        db = context;
    }

    public async Task<Product> CreateNewProductAsync(Product productData)
    {
        db.Products.Add(productData);
        await db.SaveChangesAsync();
        return productData;
    }
    public async Task<Product> UpdateExistingProductAsync(Product productData)
    {
        var product = await db.Products.FindAsync(productData.ProductId);
        if (product == null)
        {
            return default;
        }
        product.ProductName = productData.ProductName;
        product.CategoryId = productData.CategoryId;
        product.Description = productData.Description;
        product.Price = productData.Price;
        product.LastModifiedBy = 1;
        db.Attach(product);
        await db.SaveChangesAsync();
        return product;
    }
    public async Task<List<Product>> GetProductsAsync()
    {
        var products = await db.Products.ToListAsync();
        return products;
    }

    public async Task<Product> GetProductDetailAsync(int productId)
    {
        var product = await db.Products.FindAsync(productId);
        return product;
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await db.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
        return products;
    }

    public async Task DeleteProductAsync(int productId)
    {
        await db.Products.Where(p => p.ProductId == productId).ExecuteDeleteAsync();
    }
}