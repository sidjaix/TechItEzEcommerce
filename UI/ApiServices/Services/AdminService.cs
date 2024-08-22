using ApiServices.Models;
using ApiServices.Models.User;
using ApiServices.Services.IService;
using ApiServices.Utility;
using ApiServices.Utility.Enums;

namespace ApiServices.Services;

public class AdminService(IBaseService baseService) : IAdminService
{
    public async Task<ResponseDto> GetRolesAsync()
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/admin/getroles",
            ApiMethod = ApiMethod.GET
        };
        var response = await baseService.SendAsync(request);
        return response;
    }
    public async Task<ResponseDto> GetRoleDetailAsync(string roleId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/admin/GetRoleById/{roleId}",
            ApiMethod = ApiMethod.GET
        };
        var response = await baseService.SendAsync(request);
        return response;
    }
    public async Task<ResponseDto> CreateRoleAsync(RoleViewModel roleModel)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/admin/CreateRole",
            Data = roleModel,
            ApiMethod = ApiMethod.POST,
            ContentType = ContentType.Json
        };
        var response = await baseService.SendAsync(request);
        return response;
    }
    public async Task<ResponseDto> UpdateRoleAsync(RoleViewModel roleModel)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/admin/UpdateRole",
            Data = roleModel,
            ApiMethod = ApiMethod.PUT,
            ContentType = ContentType.Json
        };
        var response = await baseService.SendAsync(request);
        return response;
    }
    public async Task<ResponseDto> DeleteRoleAsync(string roleId)
    {
        var request = new RequestDto
        {
            Url = $"{ApplicationData.AuthApiBaseAddress}/api/admin/DeleteRole/{roleId}",
            ApiMethod = ApiMethod.DELETE

        };
        var response = await baseService.SendAsync(request);
        return response;
    }
}
