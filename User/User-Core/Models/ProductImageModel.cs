namespace User_Core.Models;

public partial class ProductImageModel
{
    public int ImageId { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; } = null!;
}
