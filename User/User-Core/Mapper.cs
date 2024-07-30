using User_Core.Entities;
using User_Core.Models;

namespace User_Core;

public static class UserMapper
{

    public static User MapToEntity(UserModel userModel)
    {
        return new User
        {
            UserId = userModel.UserId,
            //AdObjId = userModel.AdObjId
            Username = userModel.DisplayName,
            Email = userModel.Email,
            Password = userModel.Password,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Address = userModel.Address,
            // Add other properties as needed
        };
    }

    public static UserModel MapToDto(User user)
    {
        return new UserModel
        {
            UserId = user.UserId,
            //AdObjId = user.AdObjId
            DisplayName = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address
            // Add other properties as needed
        };
    }
}

public static class RoleMapper
{
    public static Role MapToEntity(RoleModel roleModel)
    {
        return new Role
        {
            RoleId = roleModel.RoleId,
            RoleName = roleModel.RoleName
        };
    }

    public static RoleModel MapToDto(Role role)
    {
        return new RoleModel
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        };
    }
}
