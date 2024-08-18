namespace Cart_Core.Models;

public partial class ProductModel
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; }
    public int SellingPrice { get; set; }
    public int OriginalPrice { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }

    public List<ProductImageModel> ProductImages { get; set; }
}
