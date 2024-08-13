using Product_Core.Entities;
using Product_Core.Models;

namespace Product_Core.Mapper;

public static class ProductMapperExtension
{
    public static Product MapToEntity(this ProductModel productModel)
    {
        return new Product
        {
            ProductId = productModel.ProductId,
            CategoryId = productModel.CategoryId,
            ProductName = productModel.ProductName,
            Description = productModel.Description,
            Price = productModel.Price
            // Add other properties as needed
        };
    }

    public static ProductModel MapToDto(this Product product)
    {
        return new ProductModel
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl
            // Add other properties as needed
        };
    }
}
