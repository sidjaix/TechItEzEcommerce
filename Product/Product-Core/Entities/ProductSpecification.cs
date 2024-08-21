using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("ProductSpecification")]
public class ProductSpecification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductSpecId { get; set; }
    public int ProductId { get; set; }
    public decimal Weight { get; set; }
    public string Color { get; set; }

}
