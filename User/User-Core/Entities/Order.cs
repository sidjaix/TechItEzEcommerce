using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

public partial class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }


    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual User Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderCoupon> OrderCoupons { get; set; } = new List<OrderCoupon>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [InverseProperty("Order")]
    public virtual ICollection<OrderStatus> OrderStatuses { get; set; } = new List<OrderStatus>();

    [InverseProperty("Order")]
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
