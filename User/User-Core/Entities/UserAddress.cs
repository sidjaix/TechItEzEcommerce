using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Core.Entities;

[Table("UserAddress")]
public class UserAddress
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int AddressId { get; set; }
    public bool IsDefault { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [ForeignKey(nameof(AddressId))]
    public Address Address { get; set; }
}
