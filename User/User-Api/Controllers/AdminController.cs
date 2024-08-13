using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Api.Common.Filters;
using User_Core;
using User_Core.Entities;
using User_Core.Models;
using User_Data;

namespace User_Api;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository adminRepository;
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly ResponseDto _response;
    public AdminController(IAdminRepository adminRepository, RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        this.adminRepository = adminRepository;
        _roleManager = roleManager;
        _userManager = userManager;
        _response = new ResponseDto();
    }

    [HttpPost("CreateRole")]
    [ValidateModel]
    public async Task<ActionResult<ResponseDto>> CreateRole([FromBody] RoleModel role)
    {
        var existingRole = await _roleManager.FindByIdAsync(role.RoleId);
        if (existingRole != null)
        {
            _response.Message = "Role already exist";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }

        var result = await _roleManager.CreateAsync(role.MapToEntity());
        if (!result.Succeeded)
        {
            _response.Message = "Role has not created.";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        return Created();
    }

    /// <summary>
    /// Get all Role
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetRoles")]
    public async Task<ActionResult<ResponseDto>> GetRoles()
    {
        try
        {
            var roles = await _roleManager.Roles.Select(x => x.MapToDto()).ToListAsync();
            _response.Result = roles;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
            return _response;
        }

        return Ok(_response);
    }

    /// <summary>
    /// Get role details
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns>Role</returns>
    [HttpGet("GetRoleById/{roleId}")]
    public async Task<ActionResult<ResponseDto>> GetRoleByIdAsync(string roleId)
    {
        try
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                _response.Message = "Role does not exist.";
                return NotFound(_response);
            }
            _response.Result = role;
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
            return _response;
        }
        return Ok(_response);
    }

    /// <summary>
    /// Update role
    /// </summary>
    /// <param name="roleData"></param>
    /// <returns>Role</returns>
    [HttpPut("UpdateRole")]
    [ValidateModel]
    public async Task<IActionResult> UpdateRoleAsync(RoleModel roleData)
    {
        try
        {
            // Find the role by ID
            var role = await _roleManager.FindByIdAsync(roleData.RoleId);
            if (role == null)
            {
                _response.Message = "Role does not exist";
                return NotFound(_response);
            }

            // Update the role's properties
            role.Name = roleData.RoleName;
            role.NormalizedName = roleData.RoleName.ToUpper(); // Normalize the name
            role.Description = roleData.Description;

            // Save the changes
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                _response.Result = role;
                _response.Message = "Role updated successfully";
                return Ok(_response);
            }

            // If there were errors, add them to the model state and return
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        catch (Exception ex)
        {
            _response.Message = ex.Message;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
    }

    /// <summary>
    /// Delete role by id
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteRole/{roleId}")]
    public async Task<ActionResult<ResponseDto>> DeleteRoleAsync(string roleId)
    {
        // Find the role by ID
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
        {
            _response.Message = "Role does not exist";
            return NotFound(_response);
        }
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            _response.IsSuccess = false;
            _response.Message = "Role has not deleted.";

            // If there were errors, add them to the model state and return
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        _response.Result = true;
        return _response;
    }

    [HttpPost("AssignAdminRole")]
    [ValidateModel]
    public async Task<IActionResult> AssignAdminRole(UserModel userData)
    {
        var existingUser = await _userManager.FindByIdAsync(userData.UserId);
        if (existingUser is null)
        {
            _response.Message = "User does not exist.";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        var result = await _userManager.AddToRoleAsync(existingUser, RoleStore.ADMIN);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        _response.Message = "User has assigned to role admin.";
        return Ok(_response);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole(string email, string roleName)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser is null)
        {
            _response.Message = "User does not exist.";
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        var result = await _userManager.AddToRoleAsync(existingUser, roleName);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        _response.Message = "User has assigned to role ";
        return Ok(_response);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUsers")]
    public async Task<ActionResult<ResponseDto>> GetUsers()
    {
        var users = await _userManager.Users.Select(x => x.MapToDto()).ToListAsync();
        _response.Result = users;
        return Ok(_response);
    }
}
