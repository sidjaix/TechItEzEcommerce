using User_Core.Entities;
using User_Core.Models;

namespace User_Data;

public interface IAdminRepository
{
    Task<List<Role>> GetRolesAsync();
    Task<Role> CreateRoleAsync(RoleModel role);
    Task<Role> GetRoleByIdAsync(int roleId);
    Task<Role> UpdateRoleAsync(RoleModel roleData);
    Task<bool> DeleteRoleAsync(int roleId);
}
