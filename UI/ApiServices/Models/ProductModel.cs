using System.Collections.Generic;
namespace ApiServices.Models;

public partial class ProductModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public List<ProductModel> ProductsByCategory { get; set; }
}
