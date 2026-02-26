using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using User_Core.Enums;

namespace User_Core.Entities;

public partial class User : IdentityUser
{
    [Required]
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public virtual ICollection<UserAddress> UserAddresses { get; set; } = [];
}
