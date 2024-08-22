using ApiServices.Models;
using ApiServices.Models.User;

namespace ApiServices.Services.IService;

public interface IAdminService
{
    Task<ResponseDto> GetRolesAsync();
    Task<ResponseDto> GetRoleDetailAsync(string roleId);
    Task<ResponseDto> UpdateRoleAsync(RoleViewModel roleModel);
    Task<ResponseDto> CreateRoleAsync(RoleViewModel roleModel);
    Task<ResponseDto> DeleteRoleAsync(string roleId);
}
