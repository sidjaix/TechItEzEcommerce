using ProductApplication.DTOs;
using ProductCore.Entities;

namespace ProductApplication.Mappers;

public static class CategoryMapper
{
    public static Category MapToEntity(this CategoryDto categoryModel)
    {
        return new Category(categoryModel.CategoryName, categoryModel.CategoryDescription, null)
        {

        };
    }

    public static CategoryDto MapToDto(this Category category)
    {
        return new CategoryDto
        {

        };
    }
}
