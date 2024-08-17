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
            SellingPrice = productModel.SellingPrice,
            QuantityInStock = productModel.QuantityInStock,
            ProductImages = productModel.ProductImages
        };

        return product;
    }

    public static ProductModel MapToDto(this Product product)
    {
        return new ProductModel
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            ProductName = product.ProductName,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            SellingPrice = product.SellingPrice,
            QuantityInStock = product.QuantityInStock,
            ImageUrl = product.ImageUrl,
            ProductImages = product.ProductImages.ToList()
        };
    }
}
