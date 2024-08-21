using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("ProductImage")]
public partial class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductImageId { get; set; }

    public int ProductId { get; set; }

    public string ImageUrl { get; set; }
    public string ThumbBigImageUrl { get; set; }

    [DefaultValue(true)]
    public bool IsThumbnail { get; set; } = true;

    public virtual Product Product { get; set; }
}
