namespace UserAccess.Application.Dtos;

public class LoginResponseModel
{
    public UserModel User { get; set; }
    public string Token { get; set; } = string.Empty;
}
