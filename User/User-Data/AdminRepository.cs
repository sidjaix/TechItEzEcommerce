using User_Core;
using User_Core.Entities;
using User_Core.Models;

namespace User_Data;

public class AdminRepository : IAdminRepository
{
    private readonly UserDbContext db;
    public AdminRepository(UserDbContext context)
    {
        db = context;
    }

    public List<Role> GetRoles()
    {
        var roles = db.Roles.ToList();
        return roles;
    }

    public Role CreateRole(RoleModel roleModel)
    {
        var role = RoleMapper.MapToEntity(roleModel);
        db.Roles.Add(role);
        db.SaveChanges();
        return role;
    }

    public Role GetRoleById(int roleId)
    {
        var role = db.Roles.Find(roleId);
        if (role is null)
        {
            return default;
        }
        return role;
    }

    public Role UpdateRole(RoleModel roleData)
    {
        var role = db.Roles.SingleOrDefault(u => u.RoleId == roleData.RoleId);
        role.RoleName = roleData.RoleName;
        db.Attach(role);
        db.SaveChanges();
        return role;
    }

    public void DeleteRole(int roleId)
    {
        var role = db.Roles.Find(roleId);
        db.Roles.Remove(role);
        db.SaveChanges();
    }
}
