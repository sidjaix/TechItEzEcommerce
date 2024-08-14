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
