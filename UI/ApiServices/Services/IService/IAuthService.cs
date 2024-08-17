using ApiServices.Models;
using ApiServices.Models.User;

namespace ApiServices.Services.IService;

public interface IAuthService
{
    Task<ResponseDto> LoginAsync(LoginViewModel login);
    Task<ResponseDto> RegisterAsync(RegisterViewModel register);
}
