namespace ProductCore.Entities;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int DiscountRate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
