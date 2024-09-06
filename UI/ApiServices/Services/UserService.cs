using ApiServices.Models;
using ApiServices.Models.User;
using ApiServices.Services.IService;
using ApiServices.Utility;
using Newtonsoft.Json;

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

    public async Task<UserViewModel> GetUserAsync(string userId)
    {
        UserViewModel user = new();
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/user/{userId}",
            ApiMethod = Utility.Enums.ApiMethod.GET
        };
        var response = await _baseService.SendAsync(req);
        if (response != null && response.IsSuccess)
        {
            user = JsonConvert.DeserializeObject<UserViewModel>(Convert.ToString(response.Result));
        }
        return user;
    }

    public async Task<List<AddressViewModel>> GetUserAddresses(string userId)
    {
        List<AddressViewModel> userAddresses = new();
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/user/GetUserAddresses/{userId}",
            ApiMethod = Utility.Enums.ApiMethod.GET
        };
        var response = await _baseService.SendAsync(req);
        if (response != null && response.IsSuccess)
        {
            userAddresses = JsonConvert.DeserializeObject<List<AddressViewModel>>(Convert.ToString(response.Result));
        }
        return userAddresses;
    }

    public async Task<bool> CreateAddress(AddressViewModel addressInfo)
    {
        var isAddressCreated = false;
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/user/CreateAddress",
            ApiMethod = Utility.Enums.ApiMethod.POST,
            ContentType = Utility.Enums.ContentType.Json,
            Data = addressInfo
        };
        var response = await _baseService.SendAsync(req);
        if (response is not null && response.IsSuccess)
        {
            isAddressCreated = (bool)response.Result;
        }
        return isAddressCreated;
    }

    public async Task<bool> DeleteAddressAsync(int addressId)
    {
        var isAddressDeleted = false;
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/user/DeleteAddress/{addressId}",
            ApiMethod = Utility.Enums.ApiMethod.DELETE
        };
        var response = await _baseService.SendAsync(req);
        if (response is not null && response.IsSuccess)
        {
            isAddressDeleted = (bool)response.Result;
        }
        return isAddressDeleted;
    }

    public async Task<ResponseDto> UpdateUserAsync(UserViewModel userData)
    {
        var req = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/user/",
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
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/user/{userId}",
            ApiMethod = Utility.Enums.ApiMethod.DELETE
        };
        var response = await _baseService.SendAsync(req);
        return response;
    }
}
