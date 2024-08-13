using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Product_Core.Entities;

public partial class ProductReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ReviewId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public string ReviewText { get; set; } = null!;
    public int Rating { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
}
