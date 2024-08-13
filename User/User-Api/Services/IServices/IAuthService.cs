using User_Core.Entities;
using User_Core.Models;

namespace User_Api.Services.IServices;

public interface IAuthService
{
    Task<LoginResponseModel> LoginAsync(LoginModel login);
    Task<ResponseDto> RegisterAsync(RegisterModel register);
    string GenerateToken(User user, IEnumerable<string> roles);
}
