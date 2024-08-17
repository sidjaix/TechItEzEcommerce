using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("Category")]
public partial class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public string CategoryImageUrl { get; set; }
    public virtual ICollection<Product> Products { get; set; } = [];
}
