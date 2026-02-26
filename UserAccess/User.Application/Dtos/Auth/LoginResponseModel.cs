using UserAccess.Application.Dtos.User;

namespace UserAccess.Application.Dtos.Auth;

public class LoginResponseModel
{
    public UserModel? User { get; set; }
    public string Token { get; set; } = string.Empty;
}
