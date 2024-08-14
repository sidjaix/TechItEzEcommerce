using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order_Core.Entities;

[Table("UserReview")]
public class UserReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; } = null!;
    public int OrderedProductId { get; set; }
    public int RatingValue { get; set; }
    [StringLength(500)]
    public string Comment { get; set; }

    [ForeignKey(nameof(OrderedProductId))]
    public OrderLine OrderedProduct { get; set; }
}
