namespace User_Core.Models;

public partial class ProductModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}
