using Microsoft.EntityFrameworkCore;
using Product_Core;
using Product_Core.Entities;

namespace Product_Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ProductDbContext db;
    public CategoryRepository(ProductDbContext context)
    {
        db = context;
    }
    public async Task<Category> CreateNewCategoryAsync(Category categoryModel)
    {
        categoryModel.LastModifiedBy = 1;
        categoryModel.CreatedBy = 1;
        db.Categories.Add(categoryModel);
        await db.SaveChangesAsync();
        return categoryModel;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var numberOfRowDeleted = await db.Categories.Where(p => p.CategoryId == categoryId).ExecuteDeleteAsync();
        return numberOfRowDeleted > 0;
    }

    public async Task<List<Category>> GetAllCategoryAsync()
    {
        var categories = await db.Categories.ToListAsync();
        return categories;
    }

    public async Task<Category> GetCategoryAsync(int categoryId)
    {
        var category = await db.Categories.FindAsync(categoryId);
        if (category == null)
        {
            return default;
        }
        return category;
    }

    public async Task<Category> UpdateExistingCategoryAsync(Category categoryModel)
    {
        var category = await db.Categories.FindAsync(categoryModel.CategoryId);
        if (category == null)
        {
            return default;
        }
        category.CategoryName = categoryModel.CategoryName;
        db.Attach(category);
        await db.SaveChangesAsync();
        return category;
    }
}
