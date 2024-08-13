using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace User_Core.Entities;

public partial class User : IdentityUser
{
    [Required]
    public string Name { get; set; }
}
