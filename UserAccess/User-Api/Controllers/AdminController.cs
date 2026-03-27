using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Api.Common.Filters;
using UserAccess.Application.Dtos;
using UserAccess.Application.Features.Admin.Commands;
using UserAccess.Application.Features.Admin.Queries;

namespace UserAccess.API.Controllers;

[ApiController]
[Route("api/admin")]
[Produces("application/json")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AdminController> _logger;

    public AdminController(IMediator mediator, ILogger<AdminController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("CreateRole")]
    public async Task<ActionResult<ResponseDto>> CreateRole([FromBody] RoleModel role)
    {
        _logger.LogInformation("API Call to create role with name {RoleName}", role.RoleName);
        var response = await _mediator.Send(new CreateRoleCommand { Role = role });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
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
        _logger.LogInformation("API Call to get all roles");
        var response = await _mediator.Send(new GetRolesQuery());
        return Ok(response);
    }

    /// <summary>
    /// Get role details
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns>Role</returns>
    [HttpGet("GetRoleById/{roleId}")]
    public async Task<ActionResult<ResponseDto>> GetRoleByIdAsync(string roleId)
    {
        _logger.LogInformation("API Call to get role by id {RoleId}", roleId);
        var response = await _mediator.Send(new GetRoleByIdQuery { RoleId = roleId });

        if (!response.IsSuccess)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Update role
    /// </summary>
    /// <param name="roleData"></param>
    /// <returns>Role</returns>
    [HttpPut("UpdateRole")]
    public async Task<IActionResult> UpdateRoleAsync(RoleModel roleData)
    {
        _logger.LogInformation("API Call to update role {RoleId}", roleData.RoleId);
        var response = await _mediator.Send(new UpdateRoleCommand { Role = roleData });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Delete role by id
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteRole/{roleId}")]
    public async Task<ActionResult<ResponseDto>> DeleteRoleAsync(string roleId)
    {
        _logger.LogInformation("API Call to delete role {RoleId}", roleId);
        var response = await _mediator.Send(new DeleteRoleCommand { RoleId = roleId });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("AssignAdminRole")]
    public async Task<IActionResult> AssignAdminRole(AssignAdminRoleCommand user)
    {
        _logger.LogInformation("API Call to assign admin role to user {UserId}", user.UserId);
        var response = await _mediator.Send(user);

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("AssignRole")]
    public async Task<IActionResult> AssignRole(string email, string roleName)
    {
        _logger.LogInformation("API Call to assign role {RoleName} to user {Email}", roleName, email);
        var response = await _mediator.Send(new AssignRoleCommand { Email = email, RoleName = roleName });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetUsers")]
    public async Task<ActionResult<ResponseDto>> GetUsers()
    {
        _logger.LogInformation("API Call to get all users");
        var response = await _mediator.Send(new GetUsersQuery());
        return Ok(response);
    }
}
