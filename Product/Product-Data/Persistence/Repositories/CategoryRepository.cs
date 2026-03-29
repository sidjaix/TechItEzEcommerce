using Microsoft.EntityFrameworkCore;
using ProductApplication.DTOs;
using ProductApplication.Interfaces;
using ProductApplication.Mappers;
using ProductData.Persistence;


namespace ProductData.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ProductDbContext db;
    public CategoryRepository(ProductDbContext context)
    {
        db = context;
    }
    public async Task<CategoryDto> CreateNewCategoryAsync(CategoryDto categoryModel)
    {
        var category = categoryModel.MapToEntity();
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        return category.MapToDto();
    }

    public async Task<bool> DeleteCategoryAsync(Guid categoryId)
    {
        var numberOfRowDeleted = await db.Categories
        .Where(p => p.Id == categoryId)
        .ExecuteDeleteAsync();

        return numberOfRowDeleted > 0;
    }

    public async Task<List<CategoryDto>> GetAllCategoryAsync()
    {
        var categories = await db.Categories
        .Select(x => x.MapToDto())
        .ToListAsync();

        return categories;
    }

    public async Task<CategoryDto> GetCategoryAsync(int categoryId)
    {
        var category = await db.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return default;
        }
        return category.MapToDto();
    }

    public async Task<CategoryDto> UpdateExistingCategoryAsync(CategoryDto categoryModel)
    {
        var category = await db.Categories.FindAsync(categoryModel.CategoryId);
        if (category == null)
        {
            return default;
        }

        // category.Name = categoryModel.CategoryName;
        // category.Description = categoryModel.CategoryDescription;
        // category.ImageUrl = categoryModel.CategoryImageUrl;
        db.Attach(category);
        await db.SaveChangesAsync();
        return category.MapToDto();
    }
}
