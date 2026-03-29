
namespace ProductApplication.DTOs;

public partial class CategoryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string CategoryDescription { get; set; }
    public string CategoryImageUrl { get; set; } = null!;
}
