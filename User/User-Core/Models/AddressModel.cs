using System.ComponentModel.DataAnnotations;

namespace User_Core.Models;

public class AddressModel
{
    public int AddressId { get; set; }
    [Required]
    public string UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
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
}
