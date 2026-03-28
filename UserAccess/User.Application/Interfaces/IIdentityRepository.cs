using UserAccess.Application.Dtos;

namespace User.Application.Interfaces;

public interface IIdentityRepository
{
    Task<bool> AssignAdminRoleAsync(string userId);
    Task<bool> AssignRoleAsync(string email, string role);
    Task<bool> CreateRoleAsync(RoleModel role);
    Task<bool> DeleteRoleAsync(string roleId);
    Task<bool> UpdateRoleAsync(RoleModel role);

    Task<RoleModel> GetRoleByIdAsync(string roleId);
    Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken);
    Task<bool> ValidateUserPasswordAsync(string userName, string password);
    Task<ResponseDto> CreateUserAsync(RegisterModel register);
    Task<UserModel> GetUserRolesAsync(string userName);
    Task<ResponseDto> UpdateUserAsync(UserModel userModel);
    Task<UserModel?> GetUserByIdAsync(string userId, CancellationToken cancellationToken);
}