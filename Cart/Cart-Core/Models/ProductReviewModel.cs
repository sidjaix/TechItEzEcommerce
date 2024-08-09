namespace Cart_Cores.Models;

public partial class ProductReviewModel
{
    public int ReviewId { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public string ReviewText { get; set; } = null!;
    public int Rating { get; set; }
    public DateTime CreatedOn { get; set; }
}
