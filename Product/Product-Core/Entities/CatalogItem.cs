using System;

namespace ProductCore.Entities;

public class CatalogItem
{
    // --- IDENTITY ---
    /// <summary>The unique identifier for the overarching product.</summary>
    public Guid Id { get; private set; }

    /// <summary>Human-readable URL identifier (e.g., "apple-iphone-15-pro"). Crucial for SEO.</summary>
    public string Slug { get; private set; }

    // --- SHARED DOMAIN DATA ---
    /// <summary>The base name shared by all variants (e.g., "iPhone 15 Pro").</summary>
    public string BaseName { get; private set; }

    /// <summary>Determines if the item is visible to customers on the storefront.</summary>
    public bool IsPublished { get; private set; }

    /// <summary>Foreign Key to the Category taxonomy.</summary>
    public Guid CategoryId { get; private set; }

    /// <summary>Foreign Key to the Manufacturer/Brand.</summary>
    public Guid BrandId { get; private set; }

    // --- NAVIGATION PROPERTIES ---
    /// <summary>EF Core navigation property to load the Category name.</summary>
    public Category Category { get; private set; }

    /// <summary>EF Core navigation property to load the Brand name.</summary>
    public Brand Brand { get; private set; }

    // --- AI & RAG FUEL ---
    /// <summary>A 1-2 sentence summary for grid views and initial LLM context.</summary>
    public string ShortSummary { get; private set; }

    /// <summary>The rich, multi-paragraph markdown string detailing features, materials, and use cases. This is the primary text chunked for Vector Databases.</summary>
    public string SemanticDescription { get; private set; }

    // --- ENCAPSULATED COLLECTIONS ---
    /// <summary>The specific, sellable configurations of this item (e.g., sizes/colors).</summary>
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();
    private readonly List<ProductVariant> _variants = new();

    /// <summary>Customer reviews linked to the overarching product, not a specific size/color.</summary>
    public IReadOnlyCollection<ProductReview> Reviews => _reviews.AsReadOnly();
    private readonly List<ProductReview> _reviews = new();

    /// <summary>Keywords used for exact-match database filtering prior to vector search.</summary>
    public IReadOnlyCollection<ProductTag> Tags => _tags.AsReadOnly();
    private readonly List<ProductTag> _tags = new();


    // Constructor to enforce valid state upon creation
    public CatalogItem() { }

    public CatalogItem(string baseName, string slug, Guid categoryId, Guid brandId)
    {
        Id = Guid.NewGuid();
        BaseName = baseName;
        Slug = slug;
        CategoryId = categoryId;
        BrandId = brandId;
        IsPublished = false; // Default to draft
    }

    /// <summary>Safely adds a variant while enforcing domain rules (e.g., unique SKU check).</summary>
    public void AddVariant(string sku, decimal price, string attributesJson)
    {
        if (_variants.Any(v => v.Sku == sku))
            throw new InvalidOperationException($"SKU {sku} already exists on this catalog item.");

        _variants.Add(new ProductVariant(this.Id, sku, price, attributesJson));
    }

    /// <summary>Updates the AI/Semantic profile data for the catalog item.</summary>
    public void UpdateSemanticProfile(string shortSummary, string semanticDescription)
    {
        ShortSummary = shortSummary;
        SemanticDescription = semanticDescription;
    }
}
