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

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [StringLength(50)]
    public string UnitNumber { get; set; }

    [Required]
    [StringLength(150)]
    public string AreaOrStreet { get; set; }

    [StringLength(100)]
    public string Landmark { get; set; }

    [StringLength(50)]
    public string TownOrCity { get; set; }

    [StringLength(50)]
    public string State { get; set; }

    [StringLength(6)]
    public string Pincode { get; set; }

    public bool IsDefaultAddress { get; set; }

    public Country Country { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = [];
}
