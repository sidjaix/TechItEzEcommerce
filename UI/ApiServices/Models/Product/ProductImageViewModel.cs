namespace ApiServices.Models.Product;

public class ProductImageViewModel
{
    public int ImageId { get; set; }
    public int ProductId { get; set; }
    public string ProductImageUrl { get; set; } = null!;
    public string ThumbBigImageUrl { get; set; }
    public bool? IsThumbnail { get; set; }
}
