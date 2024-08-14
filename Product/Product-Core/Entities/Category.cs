using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("Categories")]
public partial class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; } = null!;
    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Variation> Variations { get; set; } = [];
}
