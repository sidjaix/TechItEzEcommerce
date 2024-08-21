using Product_Core.Entities;
using Product_Core.Models;

namespace Product_Core.Mapper;

public static class ProductMapperExtension
{
    public static Product MapToEntity(this ProductModel productModel)
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

    public static ProductModel MapToDto(this Product product)
    {
        return new ProductModel
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            CategoryId = product.CategoryId,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            SellingPrice = product.SellingPrice,
            QuantityInStock = product.QuantityInStock,
            ImageUrl = product.ImageUrl,
            ProductImages = product.ProductImages?.Select(x => new ProductImageModel
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
