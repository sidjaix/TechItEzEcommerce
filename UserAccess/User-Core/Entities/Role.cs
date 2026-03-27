using Microsoft.AspNetCore.Identity;

namespace UserAccess.Core.Entities;

public partial class Role : IdentityRole
{
    public string Description { get; set; }
}
