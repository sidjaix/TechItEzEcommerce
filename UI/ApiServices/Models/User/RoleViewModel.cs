using System.ComponentModel.DataAnnotations;

namespace ApiServices.Models.User;

public class RoleViewModel
{
    public string RoleId { get; set; }
    [Required]
    public string RoleName { get; set; }
    public string Description { get; set; }
}
