using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

public partial class OrderCoupon
{
    [Key]
    [Column("OrderCouponID")]
    public int OrderCouponId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column("CouponID")]
    public int CouponId { get; set; }

    [ForeignKey("CouponId")]
    [InverseProperty("OrderCoupons")]
    public virtual Coupon Coupon { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderCoupons")]
    public virtual Order Order { get; set; } = null!;
}
