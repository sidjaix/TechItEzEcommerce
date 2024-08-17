using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Core.Entities;

[Table("Address")]
public partial class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }
    public int CountryId { get; set; }

    [StringLength(25)]
    public string UnitNumber { get; set; }

    [StringLength(255)]
    public string Street { get; set; } = null!;

    [Required]
    public string Address1 { get; set; } = null!;
    public string Address2 { get; set; } = null!;

    [StringLength(50)]
    public string City { get; set; } = null!;

    [StringLength(50)]
    public string State { get; set; } = null!;

    [StringLength(20)]
    public string PostalCode { get; set; } = null!;

    public bool IsShippingAddress { get; set; }

    public Country Country { get; set; }
}
