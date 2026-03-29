using ProductApplication.DTOs;
using ProductCore.Entities;

namespace ProductApplication.Mappers;

public static class ProductMapperExtension
{
    public static CatalogItem MapToEntity(this ProductDto productModel)
    {
        var product = new CatalogItem
        {
        };

        return product;
    }

    public static ProductDto MapToDto(this CatalogItem product)
    {
        return new ProductDto
        {
        };
    }
}
