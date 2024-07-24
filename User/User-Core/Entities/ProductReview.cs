using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

public partial class ProductReview
{
    [Key]
    [Column("ReviewID")]
    public int ReviewId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    public string ReviewText { get; set; } = null!;

    public int Rating { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ProductReviews")]
    public virtual User Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductReviews")]
    public virtual Product Product { get; set; } = null!;
}
