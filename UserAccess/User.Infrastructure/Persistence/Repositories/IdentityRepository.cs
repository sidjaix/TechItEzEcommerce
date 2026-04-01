using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;
using UserAccess.Infrastructure.Identity;

namespace User.Infrastructure.Persistence.Repositories;

public class IdentityRepository(ILogger<IdentityRepository> logger, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : IIdentityRepository
{
    public async Task<bool> AssignAdminRoleAsync(string username)
    {

        logger.LogInformation("Assigning admin role to user {username}", username);
        var existingUser = await userManager.FindByIdAsync(username);
        if (existingUser is null)
        {
            logger.LogWarning("User {username} not found", username);
            return false;
        }
        var isAdmin = await userManager.IsInRoleAsync(existingUser, RoleStore.ADMIN);
        if (isAdmin)
        {
            logger.LogWarning("User {username} is already an admin", username);
            return false;
        }
        var result = await userManager.AddToRoleAsync(existingUser, RoleStore.ADMIN);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to assign admin role to user {username}: {Errors}", username, result.Errors);
            return false;
        }
        logger.LogInformation("Admin role assigned to user {username}", username);
        return true;
    }

    public async Task<bool> AssignRoleAsync(string username, string roleName)
    {
        logger.LogInformation("Assigning role {RoleName} to user {username}", roleName, username);

        var existingUser = await userManager.FindByEmailAsync(username);
        if (existingUser is null)
        {
            logger.LogWarning("User {username} not found", username);
            return false;
        }
        var result = await userManager.AddToRoleAsync(existingUser, roleName);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to assign role {RoleName} to user {username}: {Errors}", roleName, username, result.Errors);
            return false;
        }
        return true;
    }

    public async Task<bool> CreateRoleAsync(RoleModel role)
    {
        logger.LogInformation("Creating a new role with name {RoleName}", role.RoleName);
        var existingRole = await roleManager.FindByNameAsync(role.RoleName);
        if (existingRole != null)
        {
            logger.LogWarning("Role {RoleName} Already exists", role.RoleName);
            return false;
        }

        var result = await roleManager.CreateAsync(role.MapToEntity());
        if (!result.Succeeded)
        {
            logger.LogWarning("Role has not created {@Errors}", result.Errors);
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteRoleAsync(string roleId)
    {
        logger.LogInformation("Deleting role {RoleId}", roleId);

        var role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            logger.LogWarning("Role {RoleId} not found", roleId);
            return false;
        }
        var result = await roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to delete role {RoleId}: {Errors}", roleId, result.Errors);
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateRoleAsync(RoleModel roleModel)
    {
        logger.LogInformation("Updating role {RoleId}", roleModel.RoleId);
        var role = await roleManager.FindByIdAsync(roleModel.RoleId);
        if (role == null)
        {
            logger.LogWarning("Role {RoleId} not found", roleModel.RoleId);
            return false;
        }

        role.Name = roleModel.RoleName;
        role.NormalizedName = roleModel.RoleName.ToUpper();
        role.Description = roleModel.Description;

        var result = await roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            logger.LogError("Failed to update role {RoleId}: {Errors}", roleModel.RoleId, result.Errors);
            return false;
        }
        return true;
    }

    public async Task<RoleModel> GetRoleByIdAsync(string roleId)
    {
        logger.LogInformation("Getting role by id {RoleId}", roleId);
        var role = await roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            logger.LogWarning("Role with id {RoleId} not found", roleId);
            return default;
        }
        return role.MapToDto();
    }

    public async Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all roles");
        var roles = await roleManager.Roles
        .AsNoTracking()
        .Select(x => x.MapToDto())
        .ToListAsync(cancellationToken);
        return roles;
    }

    public async Task<bool> ValidateUserPasswordAsync(string userName, string password)
    {
        logger.LogInformation("Validating password for user {UserName}", userName);
        var user = await userManager.FindByNameAsync(userName);
        if (user is null)
        {
            logger.LogWarning("User {UserName} not found", userName);
            return false;
        }
        var isValid = await userManager.CheckPasswordAsync(user, password);
        if (!isValid)
        {
            logger.LogWarning("Invalid password for user {UserName}", userName);
            return false;
        }
        return true;
    }

    public async Task<ResponseDto> CreateUserAsync(RegisterModel register)
    {
        var newUser = new ApplicationUser
        {
            Name = register.Name,
            UserName = register.Email,
            Email = register.Email,
            PhoneNumber = register.PhoneNumber
        };

        logger.LogInformation("Creating a new user with username {UserName}", newUser.UserName);
        var existingUser = await userManager.FindByNameAsync(newUser.UserName ?? string.Empty);
        if (existingUser != null)
        {
            logger.LogWarning(" Registration failed: Username {UserName} already exists.", newUser.UserName);
            return new ResponseDto()
            {
                IsSuccess = false,
                Message = "UserName Already Exists"
            };
        }

        var result = await userManager.CreateAsync(newUser, register.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("Failed to create user {UserName}: {Errors}", newUser.UserName, result.Errors);
            return new ResponseDto()
            {
                IsSuccess = false,
                Message = errors
            };
        }
        logger.LogInformation("User {UserName} created successfully.", newUser.UserName);

        return new ResponseDto()
        {
            IsSuccess = true,
            Message = "User created successfully."
        };
    }
    public async Task<UserModel?> GetUserRolesAsync(string userName)
    {
        var loggedinUser = await userManager.FindByNameAsync(userName);
        if (loggedinUser is null)
        {
            logger.LogWarning("User {UserName} not found", userName);
            return default;
        }
        var user = loggedinUser.MapToDto();
        var roles = await userManager.GetRolesAsync(loggedinUser);
        user.Roles = [.. roles];
        return user;
    }

    public async Task<ResponseDto> UpdateUserAsync(UserModel userModel)
    {
        logger.LogInformation("Attempting to update user {username}", userModel.UserName);
        var user = await userManager.FindByIdAsync(userModel.UserName);
        if (user is null)
        {
            logger.LogWarning("User {UserName} not found", userModel.UserName);
            return new ResponseDto()
            {
                Result = userModel,
                IsSuccess = false,
                Message = "User does not exist."
            };
        }
        user.Name = userModel.Name;
        user.DateOfBirth = userModel.DateOfBirth;
        user.Gender = userModel.Gender;
        user.Email = userModel.Email;
        user.UserName = userModel.UserName;
        user.PhoneNumber = userModel.PhoneNumber;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Message = "Failed to update user.",
                Result = result.Errors
            };
        }

        return new ResponseDto()
        {
            Result = user.MapToDto(),
            IsSuccess = true,
            Message = "User profile updated successfully."
        };
    }

    public async Task<UserModel?> GetUserByIdAsync(string username, CancellationToken cancellationToken)
    {
        var existingUser = await userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == username, cancellationToken);
        return existingUser?.MapToDto();
    }

    public string GenerateToken(UserModel user, JwtOptions jwtOptions)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Name, user.Name!)
        };

        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));

        SigningCredentials signingCred = new SigningCredentials(
            key: securityKey,
            algorithm: SecurityAlgorithms.HmacSha512Signature
        );

        var tokenExpiresOn = DateTime.UtcNow.AddMinutes(jwtOptions.ExpiresOn);

        SecurityToken securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            expires: tokenExpiresOn,
            signingCredentials: signingCred
        );
        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }

}
