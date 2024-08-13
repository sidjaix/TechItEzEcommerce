using User_Core.Entities;
using User_Core.Models;

namespace User_Core;

public static class UserMapper
{
    public static User MapToEntity(this UserModel userModel)
    {
        return new User
        {
            Id = userModel.UserId,
            UserName = userModel.UserName,
            Email = userModel.Email,
            Name = userModel.Name,
            // // Add other properties as needed
        };
    }

    public static UserModel MapToDto(this User user)
    {
        return new UserModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Name = user.Name,
            // Add other properties as needed
        };
    }
}

public static class RoleMapper
{
    public static Role MapToEntity(this RoleModel roleModel)
    {
        return new Role
        {
            //Id = roleModel.RoleId,
            Name = roleModel.RoleName,
            Description = roleModel.Description
        };
    }

    public static RoleModel MapToDto(this Role role)
    {
        return new RoleModel
        {
            RoleId = role.Id,
            RoleName = role.Name,
            Description = role.Description
        };
    }
}
