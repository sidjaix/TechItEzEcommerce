using Microsoft.EntityFrameworkCore;
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

    public async Task<List<Role>> GetRolesAsync()
    {
        var roles = await db.Roles.ToListAsync();
        return roles;
    }

    public async Task<Role> CreateRoleAsync(RoleModel roleModel)
    {
        var role = roleModel.MapToEntity();
        db.Roles.Add(role);
        await db.SaveChangesAsync();
        return role;
    }

    public async Task<Role> GetRoleByIdAsync(int roleId)
    {
        var role = await db.Roles.FindAsync(roleId);
        if (role is null)
        {
            return default;
        }
        return role;
    }

    public async Task<Role> UpdateRoleAsync(RoleModel roleData)
    {
        var role = await db.Roles.SingleOrDefaultAsync(u => u.Id == roleData.RoleId);
        role.Name = roleData.RoleName;
        db.Attach(role);
        await db.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteRoleAsync(int roleId)
    {
        var role = await db.Roles.FindAsync(roleId);
        db.Roles.Remove(role);
        var numberOfRowAffected = await db.SaveChangesAsync();
        return numberOfRowAffected > 0;
    }
}
