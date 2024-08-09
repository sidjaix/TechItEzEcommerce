namespace Product_Core.Models;

public partial class OrderCouponModel
{
    public int OrderCouponId { get; set; }

    public int OrderId { get; set; }

    public int CouponId { get; set; }
}
