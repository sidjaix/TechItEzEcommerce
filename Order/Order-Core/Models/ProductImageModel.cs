namespace Order_Core.Models;

public partial class ProductImageModel
{
    public int ProductImageId { get; set; }
    public int ProductId { get; set; }
    public string ProductImageUrl { get; set; } = null!;
    public string ThumbBigImageUrl { get; set; }
    public bool IsThumbnail { get; set; }
}
