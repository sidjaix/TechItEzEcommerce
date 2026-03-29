using ProductApplication.DTOs;
using ProductCore.Entities;

namespace ProductApplication.Mappers;

public static class CategoryMapper
{
    public static Category MapToEntity(this CategoryDto categoryModel)
    {
        return new Category
        {
            CategoryId = categoryModel.CategoryId,
            CategoryName = categoryModel.CategoryName,
            CategoryDescription = categoryModel.CategoryDescription,
            CategoryImageUrl = categoryModel.CategoryImageUrl
            // Add other properties as needed
        };
    }

    public static CategoryDto MapToDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CategoryDescription = category.CategoryDescription,
            CategoryImageUrl = category.CategoryImageUrl
            // Add other properties as needed
        };
    }
}
