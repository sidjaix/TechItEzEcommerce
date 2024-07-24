using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace User_Core.Entities;

public partial class UserRole
{
    [Key]
    [Column("UserRoleID")]
    public int UserRoleId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserRoles")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User User { get; set; } = null!;
}
