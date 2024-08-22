using Microsoft.EntityFrameworkCore;
using Product_Core;
using Product_Core.Mapper;
using Product_Core.Models;
using Product_Data.Repository.IRepository;


namespace Product_Data.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ProductDbContext db;
    public CategoryRepository(ProductDbContext context)
    {
        db = context;
    }
    public async Task<CategoryModel> CreateNewCategoryAsync(CategoryModel categoryModel)
    {
        var category = categoryModel.MapToEntity();
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        return category.MapToDto();
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var numberOfRowDeleted = await db.Categories
        .Where(p => p.CategoryId == categoryId)
        .ExecuteDeleteAsync();

        return numberOfRowDeleted > 0;
    }

    public async Task<List<CategoryModel>> GetAllCategoryAsync()
    {
        var categories = await db.Categories
        .Select(x => x.MapToDto())
        .ToListAsync();

        return categories;
    }

    public async Task<CategoryModel> GetCategoryAsync(int categoryId)
    {
        var category = await db.Categories.FindAsync(categoryId);
        if (category is null)
        {
            return default;
        }
        return category.MapToDto();
    }

    public async Task<CategoryModel> UpdateExistingCategoryAsync(CategoryModel categoryModel)
    {
        var category = await db.Categories.FindAsync(categoryModel.CategoryId);
        if (category == null)
        {
            return default;
        }
        category.CategoryName = categoryModel.CategoryName;
        category.CategoryDescription = categoryModel.CategoryDescription;
        category.CategoryImageUrl = categoryModel.CategoryImageUrl;
        db.Attach(category);
        await db.SaveChangesAsync();
        return category.MapToDto();
    }
}
