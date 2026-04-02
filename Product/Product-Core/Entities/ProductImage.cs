using System.ComponentModel;

namespace ProductCore.Entities;

public partial class ProductImage
{
    public Guid Id { get; private set; }

    /// <summary>Foreign Key to the specific variant this image depicts.</summary>
    public Guid ProductVariantId { get; private set; }

    /// <summary>The CDN or blob storage path to the image asset.</summary>
    public string ImageUrl { get; private set; }

    /// <summary>Crucial for visually impaired users, SEO, and Multimodal AI vision models.</summary>
    public string AltText { get; private set; }

    /// <summary>Flag to determine which image shows first in the gallery.</summary>
    public bool IsPrimary { get; private set; }

    /// <summary>Controls the sort order of the image gallery.</summary>
    public int DisplayOrder { get; private set; }

    public ProductImage()
    {

    }
    public ProductImage(Guid productVariantId, string imageUrl, string altText, bool isPrimary = false, int displayOrder = 0)
    {
        Id = Guid.NewGuid();
        ProductVariantId = productVariantId;
        ImageUrl = imageUrl;
        AltText = altText;
        IsPrimary = isPrimary;
        DisplayOrder = displayOrder;
    }

    internal void RemovePrimaryStatus() => IsPrimary = false;
}
