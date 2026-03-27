using Entity = UserAccess.Core.Entities;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Mappers;

public static class UserMapper
{
    public static Entity.User MapToEntity(this UserModel userModel)
    {
        return new Entity.User
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

    public static UserModel MapToDto(this Entity.User user)
    {
        return new UserModel
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
    public static Entity.Role MapToEntity(this RoleModel roleModel)
    {
        return new Entity.Role
        {
            //Id = roleModel.RoleId,
            Name = roleModel.RoleName,
            Description = roleModel.Description
        };
    }

    public static RoleModel MapToDto(this Entity.Role role)
    {
        return new RoleModel
        {
            RoleId = role.Id,
            RoleName = role.Name!,
            Description = role.Description!
        };
    }
}
