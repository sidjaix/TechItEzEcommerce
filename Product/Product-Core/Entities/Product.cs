using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("Products")]
public partial class Product
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
    [Required]
    public int CategoryId { get; set; }
    [StringLength(500)]
    public string ImageUrl { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }

    [InverseProperty(nameof(Product))]
    public virtual ICollection<ProductItem> ProductItems { get; set; } = [];
}
