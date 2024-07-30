using User_Core.Entities;
using User_Core.Models;

namespace User_Data;

public interface IAdminRepository
{
    List<Role> GetRoles();
    Role CreateRole(RoleModel role);
    Role GetRoleById(int roleId);
    Role UpdateRole(RoleModel roleData);
    void DeleteRole(int roleId);
}
