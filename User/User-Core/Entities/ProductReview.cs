using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

public partial class ProductReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReviewId { get; set; }

    public int ProductId { get; set; }

    public int CustomerId { get; set; }

    public string ReviewText { get; set; } = null!;

    public int Rating { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("ProductReviews")]
    public virtual User Customer { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductReviews")]
    public virtual Product Product { get; set; } = null!;
}
