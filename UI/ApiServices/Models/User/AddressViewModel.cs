using System.ComponentModel.DataAnnotations;

namespace ApiServices.Models.User;

public partial class AddressViewModel
{
    public int AddressId { get; set; }
    [Required]
    public string UserId { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
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

    public string FullName => $"{FirstName} {LastName}";
}
