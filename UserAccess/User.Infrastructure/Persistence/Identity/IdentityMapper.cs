using System;
using UserAccess.Application.Dtos;
using UserAccess.Infrastructure.Identity;

namespace UserAccess.Infrastructure.Identity;

public static class IdentityMapper
{
    public static ApplicationUser MapToEntity(this UserModel userModel)
    {
        return new ApplicationUser
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

    public static UserModel MapToDto(this ApplicationUser user)
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

    public static ApplicationRole MapToEntity(this RoleModel roleModel)
    {
        return new ApplicationRole
        {
            //Id = roleModel.RoleId,
            Name = roleModel.RoleName,
            Description = roleModel.Description
        };
    }


    public static RoleModel MapToDto(this ApplicationRole role)
    {
        return new RoleModel
        {
            RoleId = role.Id,
            RoleName = role.Name!,
            Description = role.Description!
        };
    }
}
