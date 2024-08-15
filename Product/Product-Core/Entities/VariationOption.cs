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


//  Table Data look a like
//  1, 1, S                 (Variation: Size)
//  2, 1, M                 (Variation: Size)
//  3, 1, L                 (Variation: Size)

//  4, 2, White             (Variation: Color)
//  5, 2, Black             (Variation: Color)

//  6, 3, Cotton            (Variation: Material)
//  7, 3, Linen             (Variation: Material)

