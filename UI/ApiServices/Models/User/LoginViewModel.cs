using System.ComponentModel.DataAnnotations;

namespace ApiServices.Models.User;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; } = false;
}
