
namespace Product_Core.Models;

public partial class CategoryModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string CategoryDescription { get; set; }
    public string CategoryImageUrl { get; set; } = null!;
}
