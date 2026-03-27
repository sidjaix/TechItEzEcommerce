using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;
using UserAccess.Infrastructure.Persistence;
using Entity = UserAccess.Core.Entities;

namespace User.Infrastructure.Persistence.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly UserDbContext db;
    private readonly ILogger<IdentityRepository> _logger;
    private readonly UserManager<Entity.User> _userManager;
    private readonly RoleManager<Entity.Role> _roleManager;
    public IdentityRepository(ILogger<IdentityRepository> logger, UserManager<Entity.User> userManager, RoleManager<Entity.Role> roleManager, UserDbContext dbContext)
    {
        db = dbContext;
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssignAdminRoleAsync(string userId)
    {

        _logger.LogInformation("Assigning admin role to user {UserId}", userId);
        var existingUser = await _userManager.FindByIdAsync(userId);
        if (existingUser is null)
        {
            _logger.LogWarning("User {UserId} not found", userId);
            return false;
        }
        var isAdmin = await _userManager.IsInRoleAsync(existingUser, RoleStore.ADMIN);
        if (isAdmin)
        {
            _logger.LogWarning("User {UserId} is already an admin", userId);
            return false;
        }
        var result = await _userManager.AddToRoleAsync(existingUser, RoleStore.ADMIN);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to assign admin role to user {UserId}: {Errors}", userId, result.Errors);
            return false;
        }
        _logger.LogInformation("Admin role assigned to user {UserId}", userId);
        return true;
    }

    public async Task<bool> AssignRoleAsync(string userId, string roleName)
    {
        _logger.LogInformation("Assigning role {RoleName} to user {UserId}", roleName, userId);

        var existingUser = await _userManager.FindByIdAsync(userId);
        if (existingUser is null)
        {
            _logger.LogWarning("User {UserId} not found", userId);
            return false;
        }
        var result = await _userManager.AddToRoleAsync(existingUser, roleName);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to assign role {RoleName} to user {UserId}: {Errors}", roleName, userId, result.Errors);
            return false;
        }
        return true;
    }

    public async Task<bool> CreateRoleAsync(RoleModel role)
    {
        _logger.LogInformation("Creating a new role with name {RoleName}", role.RoleName);
        var existingRole = await _roleManager.FindByNameAsync(role.RoleName);
        if (existingRole != null)
        {
            _logger.LogWarning("Role {RoleName} Already exists", role.RoleName);
            return false;
        }

        var result = await _roleManager.CreateAsync(role.MapToEntity());
        if (!result.Succeeded)
        {
            _logger.LogWarning("Role has not created {@Errors}", result.Errors);
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteRoleAsync(string roleId)
    {
        _logger.LogInformation("Deleting role {RoleId}", roleId);

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            _logger.LogWarning("Role {RoleId} not found", roleId);
            return false;
        }
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            _logger.LogError("Failed to delete role {RoleId}: {Errors}", roleId, result.Errors);
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateRoleAsync(RoleModel roleModel)
    {
        _logger.LogInformation("Updating role {RoleId}", roleModel.RoleId);
        var role = await _roleManager.FindByIdAsync(roleModel.RoleId);
        if (role == null)
        {
            _logger.LogWarning("Role {RoleId} not found", roleModel.RoleId);
            return false;
        }

        role.Name = roleModel.RoleName;
        role.NormalizedName = roleModel.RoleName.ToUpper();
        role.Description = roleModel.Description;

        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            _logger.LogError("Failed to update role {RoleId}: {Errors}", roleModel.RoleId, result.Errors);
            return false;
        }
        return true;
    }

    public async Task<RoleModel> GetRoleByIdAsync(string roleId)
    {
        _logger.LogInformation("Getting role by id {RoleId}", roleId);
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            _logger.LogWarning("Role with id {RoleId} not found", roleId);
            return default;
        }
        return role.MapToDto();
    }

    public async Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all roles");
        var roles = await _roleManager.Roles
        .AsNoTracking()
        .Select(x => x.MapToDto())
        .ToListAsync(cancellationToken);
        return roles;
    }

    public async Task<bool> ValidateUserPasswordAsync(string userName, string password)
    {
        _logger.LogInformation("Validating password for user {UserName}", userName);
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            _logger.LogWarning("User {UserName} not found", userName);
            return false;
        }
        var isValid = await _userManager.CheckPasswordAsync(user, password);
        if (!isValid)
        {
            _logger.LogWarning("Invalid password for user {UserName}", userName);
            return false;
        }
        return true;
    }

    public async Task<ResponseDto> CreateUserAsync(RegisterModel register)
    {
        var newUser = new Entity.User
        {
            Name = register.Name,
            UserName = register.Email,
            Email = register.Email,
            PhoneNumber = register.PhoneNumber
        };

        _logger.LogInformation("Creating a new user with username {UserName}", newUser.UserName);
        var existingUser = await _userManager.FindByNameAsync(newUser.UserName ?? string.Empty);
        if (existingUser != null)
        {
            _logger.LogWarning(" Registration failed: Username {UserName} already exists.", newUser.UserName);
            return new ResponseDto()
            {
                IsSuccess = false,
                Message = "UserName Already Exists"
            };
        }

        var result = await _userManager.CreateAsync(newUser, register.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Failed to create user {UserName}: {Errors}", newUser.UserName, result.Errors);
            return new ResponseDto()
            {
                IsSuccess = false,
                Message = errors
            };
        }
        _logger.LogInformation("User {UserName} created successfully.", newUser.UserName);

        return new ResponseDto()
        {
            IsSuccess = true,
            Message = "User created successfully."
        };
    }
    public async Task<UserModel?> GetUserRolesAsync(string userName)
    {
        var loggedinUser = await _userManager.FindByNameAsync(userName);
        if (loggedinUser is null)
        {
            _logger.LogWarning("User {UserName} not found", userName);
            return default;
        }
        var user = loggedinUser.MapToDto();
        var roles = await _userManager.GetRolesAsync(loggedinUser);
        user.Roles = [.. roles];
        return user;
    }
}
