using System;

namespace ProductCore.Entities;

public class ProductVariant
{
    // --- IDENTITY & RELATIONSHIP ---
    /// <summary>The unique identifier for this specific configuration.</summary>
    public Guid Id { get; private set; }

    /// <summary>Foreign Key pointing back to the parent CatalogItem.</summary>
    public Guid CatalogItemId { get; private set; }

    // --- TRANSACTIONAL DATA ---
    /// <summary>Stock Keeping Unit. The unique barcode/internal ID for this specific item.</summary>
    public string Sku { get; private set; }

    /// <summary>The cost of this specific variant (e.g., XXL might cost more than M).</summary>
    public decimal Price { get; private set; }

    /// <summary>The current physical inventory count for this specific configuration.</summary>
    public int StockQuantity { get; private set; }

    // --- CONFIGURATION ---
    /// <summary>A serialized JSON object defining what makes this variant unique. e.g., {"Color": "Blue", "Storage": "256GB"}. Easy to parse dynamically.</summary>
    public string AttributesJson { get; private set; }

    // --- VISUALS ---
    /// <summary>Images specific to this configuration (e.g., only showing the Blue phone).</summary>
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();
    private readonly List<ProductImage> _images = new();

    public ProductVariant()
    {

    }

    public ProductVariant(Guid catalogItemId, string sku, decimal price, string attributesJson)
    {
        Id = Guid.NewGuid();
        CatalogItemId = catalogItemId;
        Sku = sku;
        Price = price;
        AttributesJson = attributesJson;
    }
}
