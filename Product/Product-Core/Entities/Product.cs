using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

public partial class Product : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    [StringLength(255)]
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string Description { get; set; }
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    [StringLength(500)]
    public string ImageUrl { get; set; }
}
