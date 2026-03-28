using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAccess.Infrastructure.Identity;

[Table("Role")]
public partial class ApplicationRole : IdentityRole
{
    public string Description { get; set; }
}
