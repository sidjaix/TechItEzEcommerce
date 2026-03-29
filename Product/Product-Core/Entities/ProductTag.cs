using System;

namespace ProductCore.Entities;

public class ProductTag
{
    public Guid Id { get; private set; }

    /// <summary>Foreign Key linking the tag to the catalog item.</summary>
    public Guid CatalogItemId { get; private set; }

    /// <summary>The normalized keyword (e.g., "waterproof", "eco-friendly").</summary>
    public string Name { get; private set; }

    public ProductTag() { } // Parameterless constructor for EF Core

    public ProductTag(Guid catalogItemId, string name)
    {
        Id = Guid.NewGuid();
        CatalogItemId = catalogItemId;
        Name = name;
    }
}
