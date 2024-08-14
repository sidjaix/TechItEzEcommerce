using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("VariationOption")]
public class VariationOption
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string VariationId { get; set; }
    public string Value { get; set; }

    [ForeignKey(nameof(VariationId))]
    public Variation Variation { get; set; }
}
