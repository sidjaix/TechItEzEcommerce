using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Core.Entities;

[Table("Country")]
public class Country
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CountryId { get; set; }
    [StringLength(80)]
    public string Name { get; set; }
    [StringLength(80)]
    public string UpperName { get; set; }
    [StringLength(2)]
    public string ISO { get; set; }
    [StringLength(3)]
    public string ISO3 { get; set; }
    public int CountryCode { get; set; }
    public int PhoneCode { get; set; }
}
