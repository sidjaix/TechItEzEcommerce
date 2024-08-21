namespace ApiServices.Models.Product;

public partial class ProductViewModel
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; }
    public int SellingPrice { get; set; }
    public int OriginalPrice { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }
    public string StockAvailability => QuantityInStock > 0 ? "In Stock" : "Out of stock";

    public List<ProductImageViewModel> ProductImages { get; set; } = [];
    public List<ProductViewModel> ProductsByCategory { get; set; } = [];

}
