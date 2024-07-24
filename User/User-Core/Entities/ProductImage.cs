using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace User_Core.Entities;

public partial class ProductImage
{
    [Key]
    [Column("ImageID")]
    public int ImageId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(255)]
    public string ImageUrl { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    public virtual Product Product { get; set; } = null!;
}
