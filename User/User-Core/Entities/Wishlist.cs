using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

[Table("Wishlist")]
public partial class Wishlist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WishlistId { get; set; }

    public int CustomerId { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Wishlists")]
    public virtual User Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("Wishlists")]
    public virtual Product Product { get; set; } = null!;
}
