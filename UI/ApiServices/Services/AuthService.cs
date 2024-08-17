using ApiServices.Models.User;
using ApiServices.Models;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;

namespace ApiServices.Services;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService = null;
    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> LoginAsync(LoginViewModel login)
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/auth/login",
            ApiMethod = ApiMethod.POST,
            ContentType = ContentType.Json,
            Data = login
        };
        var response = await _baseService.SendAsync(req, false);
        return response;
    }

    public async Task<ResponseDto> RegisterAsync(RegisterViewModel register)
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/auth/register",
            ApiMethod = ApiMethod.POST,
            ContentType = ContentType.Json,
            Data = register
        };
        var response = await _baseService.SendAsync(req, false);
        return response;
    }
}
