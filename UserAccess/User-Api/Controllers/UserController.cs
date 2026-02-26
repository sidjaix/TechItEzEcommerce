using MediatR;
using Microsoft.AspNetCore.Mvc;
using User_Api.Common.Filters;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.Address;
using UserAccess.Application.Dtos.User;
using UserAccess.Application.Features.User.Commands;
using UserAccess.Application.Features.User.Queries;

namespace UserAccess.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get user/customer data
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>UserModel</returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<ResponseDto>> GetUserByIdAsync(string userId)
    {
        _logger.LogInformation("API Call to get user by id {UserId}", userId);
        var response = await _mediator.Send(new GetUserByIdQuery { UserId = userId });

        if (!response.IsSuccess)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Update user/customer info
    /// </summary>
    /// <param name="userData"></param>
    /// <returns>UserModel</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UserModel userData)
    {
        _logger.LogInformation("API Call to update user {UserId}", userData.UserId);
        var response = await _mediator.Send(new UpdateUserCommand { User = userData });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Get user addresses
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("GetUserAddresses/{userid}")]
    public async Task<IActionResult> GetUserAddresses(string userId)
    {
        _logger.LogInformation("API Call to get addresses for user {UserId}", userId);
        var response = await _mediator.Send(new GetUserAddressesQuery { UserId = userId });

        if (!response.IsSuccess)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet("GetAddress/{addressId}")]
    public async Task<ActionResult> GetAddress(int addressId)
    {
        _logger.LogInformation("API Call to get address {AddressId}", addressId);
        var response = await _mediator.Send(new GetAddressQuery { AddressId = addressId });

        if (!response.IsSuccess)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpPost("SaveAddress")]
    public async Task<ActionResult<ResponseDto>> SaveAddress(AddressModel addressInfo)
    {
        _logger.LogInformation("API Call to save address");
        var response = await _mediator.Send(new SaveAddressCommand { Address = addressInfo });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Delete address
    /// </summary>
    /// <param name="addressId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteAddress/{addressId}")]
    public async Task<ActionResult> DeleteAddress(int addressId)
    {
        _logger.LogInformation("API Call to delete address {AddressId}", addressId);
        var response = await _mediator.Send(new DeleteAddressCommand { AddressId = addressId });

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
