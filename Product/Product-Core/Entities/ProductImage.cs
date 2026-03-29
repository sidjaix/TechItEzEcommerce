using System.ComponentModel;

namespace ProductCore.Entities;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public int ProductId { get; set; }

    public string ImageUrl { get; set; }
    public string ThumbBigImageUrl { get; set; }

    [DefaultValue(true)]
    public bool IsThumbnail { get; set; } = true;

    public virtual Product Product { get; set; }
}
