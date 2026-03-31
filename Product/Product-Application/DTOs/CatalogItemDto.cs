
namespace ProductApplication.DTOs;

public partial class CatalogItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string ShortSummary { get; set; }

    // Fully resolved relational names
    public string CategoryName { get; set; }
    public string BrandName { get; set; }

    public decimal StartingPrice { get; set; }
    public string PrimaryImageUrl { get; set; }
}
