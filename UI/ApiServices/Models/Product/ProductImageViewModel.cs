namespace ApiServices.Models.Product;

public class ProductImageViewModel
{
    public int ImageId { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool? IsThumbnail { get; set; }
}
