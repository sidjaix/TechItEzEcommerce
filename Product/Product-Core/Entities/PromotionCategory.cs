using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("PromotionCategory")]
public partial class PromotionCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PromotionId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; }

    public virtual Promotion Promotion { get; set; }
}
