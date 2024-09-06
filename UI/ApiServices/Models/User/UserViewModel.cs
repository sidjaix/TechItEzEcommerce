using ApiServices.Enums;
using ApiServices.Utility;
using System.ComponentModel.DataAnnotations;

namespace ApiServices.Models.User;

public class UserViewModel
{
    [Required]
    public string UserId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string UserName { get; set; }

    [Required]
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Invalid Date format")]
    [DateNotInFuture(ErrorMessage = "Date of Birth cannot be in the future.")]
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
