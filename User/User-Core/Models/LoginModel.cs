using System.ComponentModel.DataAnnotations;

namespace User_Core.Models;

public class LoginModel
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; } = false;
}
