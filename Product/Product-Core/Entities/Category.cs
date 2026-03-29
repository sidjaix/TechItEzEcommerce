namespace ProductCore.Entities;

public partial class Category
{
    public Guid Id { get; private set; }

    /// <summary>Display name (e.g., "Laptops").</summary>
    public string Name { get; private set; }

    /// <summary>Contextual description of the category. Used by AI to understand the broader context of items within it.</summary>
    public string Description { get; private set; }

    /// <summary>Self-referencing Foreign Key to allow infinite nesting (e.g., Electronics -> Computers -> Laptops).</summary>
    public Guid? ParentCategoryId { get; private set; }

    // Navigation properties for EF Core tree traversal
    public Category ParentCategory { get; private set; }
    public IReadOnlyCollection<Category> SubCategories => _subCategories.AsReadOnly();
    private readonly List<Category> _subCategories = new();

    // Constructor
    public Category()
    {

    }

    public Category(string name, string description, Guid? parentCategoryId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }
}
