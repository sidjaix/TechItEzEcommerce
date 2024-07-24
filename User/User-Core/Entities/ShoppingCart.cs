using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

[Table("ShoppingCart")]
public partial class ShoppingCart
{
    [Key]
    [Column("CartID")]
    public int CartId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ShoppingCarts")]
    public virtual User Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ShoppingCarts")]
    public virtual Product Product { get; set; } = null!;
}
