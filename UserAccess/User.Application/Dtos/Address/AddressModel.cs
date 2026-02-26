using System.ComponentModel.DataAnnotations;

namespace UserAccess.Application.Dtos.Address;

public class AddressModel
{
    public int AddressId { get; set; }

    public string UserId { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string UnitNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(150)]
    public string AreaOrStreet { get; set; } = string.Empty;

    [StringLength(100)]
    public string Landmark { get; set; } = string.Empty;

    [StringLength(50)]
    public string TownOrCity { get; set; } = string.Empty;

    [StringLength(50)]
    public string State { get; set; } = string.Empty;

    [StringLength(6)]
    public string Pincode { get; set; } = string.Empty;
    public bool IsDefaultAddress { get; set; }
}
