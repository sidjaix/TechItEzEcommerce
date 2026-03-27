using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UserAccess.Core.Enums;

namespace UserAccess.Core.Entities;

public partial class User : IdentityUser
{
    [Required]
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public virtual ICollection<UserAddress> UserAddresses { get; set; } = [];
}
