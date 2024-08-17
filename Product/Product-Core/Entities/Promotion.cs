using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("Promotion")]
public partial class Promotion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PromotionId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int DiscountRate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
