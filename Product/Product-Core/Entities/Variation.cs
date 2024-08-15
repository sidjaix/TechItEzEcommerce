using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Core.Entities;

[Table("Variation")]
public class Variation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int Name { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }

    [InverseProperty("Variation")]
    public virtual ICollection<VariationOption> VariationOptions { get; set; } = [];
}

//  Table Data look a like
//  1, 1, Size              (Category: Clothing)
//  2, 1, Color             (Category: Clothing)
//  3, 1, Material          (Category: Clothing)

//  4, 2, Screen Size       (Category: Mobile Phone)
//  5, 2, Storage Capacity  (Category: Mobile Phone)
//  6, 2, Color             (Category: Mobile Phone)

