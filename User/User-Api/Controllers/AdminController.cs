using Microsoft.AspNetCore.Mvc;
using User_Api.Common.Filters;
using User_Core.Entities;
using User_Core.Models;
using User_Data;

namespace User_Api;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminRepository adminRepository;
    public AdminController(IAdminRepository adminRepository)
    {
        this.adminRepository = adminRepository;
    }

    /// <summary>
    /// Get Role of the application
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetRoles")]
    public IActionResult GetRoles()
    {
        var roles = adminRepository.GetRoles();
        return Ok(roles);
    }

    /// <summary>
    /// Create Role in the application
    /// </summary>
    /// <param name="roleModel"></param>
    /// <returns>Role</returns>
    [HttpPost("CreateRole")]
    [ValidateModel]
    public ActionResult<Role> CreateRole([FromBody] RoleModel roleModel)
    {
        var createdRole = adminRepository.CreateRole(roleModel);
        return CreatedAtAction(nameof(GetRoleById), new { roleId = createdRole.RoleId }, createdRole);
    }

    /// <summary>
    /// Get Role data
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns>Role</returns>
    [HttpGet("GetRoleById/{roleId}")]
    public ActionResult<Role> GetRoleById(int roleId)
    {
        var role = adminRepository.GetRoleById(roleId);
        if (role is null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    /// <summary>
    /// Update Role
    /// </summary>
    /// <param name="roleData"></param>
    /// <returns>Role</returns>
    [HttpPut("UpdateRole")]
    public ActionResult<Role> UpdateRole(RoleModel roleData)
    {
        var role = adminRepository.UpdateRole(roleData);
        return Ok(role);
    }

    /// <summary>
    /// Delete role by id
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteRole/{roleId}")]
    public ActionResult<Role> DeleteRole(int roleId)
    {
        adminRepository.DeleteRole(roleId);
        return Ok();
    }
}
