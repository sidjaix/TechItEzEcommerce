namespace User_Core.Models;

public partial class CouponModel
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = null!;
    public decimal DiscountAmount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int? ProductId { get; set; }
    public int? CategoryId { get; set; }
}
