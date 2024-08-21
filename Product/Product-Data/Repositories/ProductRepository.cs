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
        existingProduct.OriginalPrice = productData.OriginalPrice;
        existingProduct.SellingPrice = productData.SellingPrice;
        existingProduct.QuantityInStock = productData.QuantityInStock;
        existingProduct.ImageUrl = productData.ImageUrl;
        existingProduct.ProductImages = productData.ProductImages?
                                        .Select(x => new ProductImage
                                        {
                                            ProductId = productData.ProductId,
                                            ImageUrl = x.ProductImageUrl,
                                            ThumbBigImageUrl = x.ThumbBigImageUrl,
                                            IsThumbnail = x.IsThumbnail,
                                            ProductImageId = x.ProductImageId
                                        }).ToList();

        db.Attach(existingProduct);
        await db.SaveChangesAsync();
        return existingProduct.MapToDto();
    }
    public async Task<List<ProductModel>> GetProductsAsync()
    {
        var productQuery = from product in db.Products
                           select new ProductModel
                           {
                               ProductId = product.ProductId,
                               ProductName = product.ProductName,
                               Description = product.Description,
                               SellingPrice = product.SellingPrice,
                               OriginalPrice = product.OriginalPrice,
                               QuantityInStock = product.QuantityInStock,
                               ImageUrl = product.ImageUrl,
                               CategoryId = product.CategoryId,
                               CategoryName = product.Category.CategoryName,
                               ProductImages = product.ProductImages.Select(x => new ProductImageModel
                               {
                                   ProductId = product.ProductId,
                                   ProductImageUrl = x.ImageUrl,
                                   ThumbBigImageUrl = x.ThumbBigImageUrl,
                                   IsThumbnail = x.IsThumbnail,
                                   ProductImageId = x.ProductImageId
                               }).ToList()
                           };
        var products = await productQuery
        .AsSplitQuery()
        .ToListAsync();

        return products;
    }
    public async Task<ProductModel> GetProductDetailAsync(int productId)
    {
        var productQuery = from p in db.Products
                           where p.ProductId == productId
                           select new ProductModel
                           {
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               Description = p.Description,
                               SellingPrice = p.SellingPrice,
                               OriginalPrice = p.OriginalPrice,
                               QuantityInStock = p.QuantityInStock,
                               ImageUrl = p.ImageUrl,
                               CategoryId = p.CategoryId,
                               ProductImages = p.ProductImages.Select(pi => new ProductImageModel
                               {
                                   ProductId = p.ProductId,
                                   ProductImageUrl = pi.ImageUrl,
                                   ThumbBigImageUrl = pi.ThumbBigImageUrl,
                                   IsThumbnail = pi.IsThumbnail,
                                   ProductImageId = pi.ProductImageId
                               }).ToList()
                           };
        var product = await productQuery
        .AsSplitQuery()
        .SingleOrDefaultAsync();

        return product ?? default;
    }
    public async Task<List<ProductModel>> GetProductsByCategoryAsync(int categoryId)
    {
        var productsQuery = from product in db.Products
                            where product.CategoryId == categoryId
                            select new ProductModel
                            {
                                ProductId = product.ProductId,
                                ProductName = product.ProductName,
                                Description = product.Description,
                                SellingPrice = product.SellingPrice,
                                OriginalPrice = product.OriginalPrice,
                                QuantityInStock = product.QuantityInStock,
                                ImageUrl = product.ImageUrl,
                                CategoryId = product.CategoryId,
                                ProductImages = product.ProductImages.Select(x => new ProductImageModel
                                {
                                    ProductId = product.ProductId,
                                    ProductImageUrl = x.ImageUrl,
                                    ThumbBigImageUrl = x.ThumbBigImageUrl,
                                    IsThumbnail = x.IsThumbnail,
                                    ProductImageId = x.ProductImageId
                                }).ToList()
                            };

        var products = await productsQuery
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