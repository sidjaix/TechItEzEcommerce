using Microsoft.AspNetCore.Identity;

namespace User_Core.Entities;

public partial class Role : IdentityRole
{
    public string Description { get; set; }
}
