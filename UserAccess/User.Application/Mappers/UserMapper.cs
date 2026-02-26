using User_Core.Entities;
using UserAccess.Application.Dtos.User;

namespace UserAccess.Application.Mappers;

public static class UserMapper
{
    public static User_Core.Entities.User MapToEntity(this Dtos.User.UserModel userModel)
    {
        return new User_Core.Entities.User
        {
            Id = userModel.UserId,
            UserName = userModel.UserName,
            Email = userModel.Email,
            Name = userModel.Name,
            DateOfBirth = userModel.DateOfBirth,
            Gender = userModel.Gender,
            PhoneNumber = userModel.PhoneNumber
            // Add other properties as needed
        };
    }

    public static Dtos.User.UserModel MapToDto(this User_Core.Entities.User user)
    {
        return new Dtos.User.UserModel
        {
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Name = user.Name!,
            DateOfBirth = user.DateOfBirth,
            Gender = user.Gender,
            PhoneNumber = user.PhoneNumber!
            // Add other properties as needed
        };
    }
}

public static class RoleMapper
{
    public static Role MapToEntity(this Dtos.User.RoleModel roleModel)
    {
        return new Role
        {
            //Id = roleModel.RoleId,
            Name = roleModel.RoleName,
            Description = roleModel.Description
        };
    }

    public static Dtos.User.RoleModel MapToDto(this Role role)
    {
        return new Dtos.User.RoleModel
        {
            RoleId = role.Id,
            RoleName = role.Name!,
            Description = role.Description!
        };
    }
}
