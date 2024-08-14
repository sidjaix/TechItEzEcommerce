using ApiServices.Models;
using ApiServices.Services.IService;
using ApiServices.Utility;

namespace ApiServices.Services;

public class UserService : IUserService
{
    private readonly IBaseService _baseService;

    public UserService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> GetUsersAsync()
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/user",
            ApiMethod = Utility.Enums.ApiMethod.GET
        };
        var response = await _baseService.SendAsync(req);
        return response;
    }

    public async Task<ResponseDto> GetUserAsync(string userId)
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/user/{userId}",
            ApiMethod = Utility.Enums.ApiMethod.GET
        };
        var response = await _baseService.SendAsync(req);
        return response;
    }

    public async Task<ResponseDto> UpdateUserAsync(UserModel userData)
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/user/",
            ApiMethod = Utility.Enums.ApiMethod.PUT,
            ContentType = Utility.Enums.ContentType.Json,
            Data = userData
        };
        var response = await _baseService.SendAsync(req);
        return response;
    }

    public async Task<ResponseDto> DeleteUserAsync(string userId)
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/user/{userId}",
            ApiMethod = Utility.Enums.ApiMethod.DELETE
        };
        var response = await _baseService.SendAsync(req);
        return response;
    }
}
