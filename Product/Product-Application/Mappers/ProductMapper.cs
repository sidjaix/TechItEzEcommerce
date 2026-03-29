using ProductApplication.DTOs;
using ProductCore.Entities;

namespace ProductApplication.Mappers;

public static class ProductMapperExtension
{
    public static Product MapToEntity(this ProductDto productModel)
    {
        var product = new Product
        {
            ProductId = productModel.ProductId,
            CategoryId = productModel.CategoryId,
            ProductName = productModel.ProductName,
            Description = productModel.Description,
            OriginalPrice = productModel.OriginalPrice,
            ImageUrl = productModel.ImageUrl,
            SellingPrice = productModel.SellingPrice,
            QuantityInStock = productModel.QuantityInStock,
            ProductImages = productModel.ProductImages?.Select(x => new ProductImage
            {
                ProductId = productModel.ProductId,
                ImageUrl = x.ProductImageUrl,
                ThumbBigImageUrl = x.ThumbBigImageUrl,
                IsThumbnail = x.IsThumbnail,
                ProductImageId = x.ProductImageId
            }).ToList()
        };

        return product;
    }

    public static ProductDto MapToDto(this Product product)
    {
        return new ProductDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            CategoryId = product.CategoryId,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            SellingPrice = product.SellingPrice,
            QuantityInStock = product.QuantityInStock,
            ImageUrl = product.ImageUrl,
            ProductImages = product.ProductImages?.Select(x => new ProductImageDto
            {
                ProductId = product.ProductId,
                ProductImageUrl = x.ImageUrl,
                ThumbBigImageUrl = x.ThumbBigImageUrl,
                IsThumbnail = x.IsThumbnail,
                ProductImageId = x.ProductImageId
            }).ToList()
        };
    }
}
