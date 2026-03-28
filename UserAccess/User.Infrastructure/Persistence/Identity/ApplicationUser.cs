using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserAccess.Core.Enums;

namespace UserAccess.Infrastructure.Identity;

[Table("User")]
public partial class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
}
