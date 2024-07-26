using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

public partial class Coupon : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CouponId { get; set; }

    [StringLength(20)]
    public string CouponCode { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpiryDate { get; set; }

    public int? ProductId { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Coupons")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Coupon")]
    public virtual ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();

    [ForeignKey("ProductId")]
    [InverseProperty("Coupons")]
    public virtual Product? Product { get; set; }
}
