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

    public ProductVariant(Guid catalogItemId, string sku, decimal price, int stockQuantity, string attributesJson)
    {
        Id = Guid.NewGuid();
        CatalogItemId = catalogItemId;
        Sku = sku;
        Price = price;
        StockQuantity = stockQuantity;
        AttributesJson = attributesJson;
    }

    /// <summary>Safely reduces stock, throwing an exception if inventory would go negative.</summary>
    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity to decrease must be greater than zero.");
        }

        if (StockQuantity - quantity < 0)
        {
            throw new InvalidOperationException($"Insufficient stock for SKU {Sku}. Available: {StockQuantity}, Requested: {quantity}");
        }

        StockQuantity -= quantity;
    }

    /// <summary>Increases available stock.</summary>
    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity to increase must be greater than zero.");
        }

        StockQuantity += quantity;
    }

    /// <summary>Updates the price of the variant.</summary>
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.");
        }

        Price = newPrice;
    }

    /// <summary>Adds an image specific to this variant.</summary>
    public void AddImage(string imageUrl, string altText, bool isPrimary, int displayOrder)
    {
        // If this new image is primary, we must unset any existing primary images for this variant
        if (isPrimary)
        {
            foreach (var img in _images.Where(i => i.IsPrimary))
            {
                img.RemovePrimaryStatus();
            }
        }

        _images.Add(new ProductImage(this.Id, imageUrl, altText, isPrimary, displayOrder));
    }
}
