namespace ApiServices.Models.User;

public class LoginResponseModel
{
    public UserViewModel User { get; set; }
    public string Token { get; set; }
}
