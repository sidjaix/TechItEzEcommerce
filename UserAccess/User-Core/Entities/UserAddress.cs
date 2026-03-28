using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAccess.Core.Entities;

[Table("UserAddress")]
public class UserAddress
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
}
