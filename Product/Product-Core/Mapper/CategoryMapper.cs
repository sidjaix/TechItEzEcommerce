using Product_Core.Entities;
using Product_Core.Models;
using System;

namespace Product_Core.Mapper;

public static class CategoryMapper
{
    public static Category MapToEntity(this CategoryModel categoryModel)
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

    public static CategoryModel MapToDto(this Category category)
    {
        return new CategoryModel
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            CategoryDescription = category.CategoryDescription,
            CategoryImageUrl = category.CategoryImageUrl
            // Add other properties as needed
        };
    }
}
