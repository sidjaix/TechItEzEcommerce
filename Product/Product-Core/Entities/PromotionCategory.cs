namespace ProductCore.Entities;

public partial class PromotionCategory
{
    public int PromotionId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public virtual Promotion Promotion { get; set; }
}
