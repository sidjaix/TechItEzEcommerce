using ApiServices.Models;

namespace ApiServices.Services.IService;

public interface IAuthService
{
    Task<ResponseDto> LoginAsync(LoginModel login);
    Task<ResponseDto> RegisterAsync(RegisterModel register);
}
