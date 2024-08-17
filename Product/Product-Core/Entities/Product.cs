using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("Product")]
public partial class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public int SellingPrice { get; set; }
    public int OriginalPrice { get; set; }
    public int QuantityInStock { get; set; }
    public string ImageUrl { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
