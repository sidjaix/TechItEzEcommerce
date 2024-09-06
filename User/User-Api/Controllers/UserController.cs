using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Api.Common.Filters;
using User_Core;
using User_Core.Entities;
using User_Core.Models;
using User_Data.Repository;
using User_Data.Repository.IRepository;

namespace User_Api.Controllers;


[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly ResponseDto _response;

    public UserController(IUserRepository userRepository, UserManager<User> userManager, ResponseDto response)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _response = response;
    }

    /// <summary>
    /// Get user/customer data
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>UserModel</returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<ResponseDto>> GetUserByIdAsync(string userId)
    {
        var existingUser = await _userManager.FindByIdAsync(userId);
        if (existingUser is null)
        {
            _response.Message = $"user does not exist with specified Id {userId}";
            _response.IsSuccess = false;
            return NotFound(_response);
        }
        _response.Result = existingUser.MapToDto();
        return Ok(_response);
    }

    /// <summary>
    /// Update user/customer info
    /// </summary>
    /// <param name="userData"></param>
    /// <returns>UserModel</returns>
    [HttpPut]
    [ValidateModel]
    public async Task<IActionResult> UpdateUser(UserModel userData)
    {
        var existingUser = await _userManager.FindByIdAsync(userData.UserId);
        if (existingUser is null)
        {
            _response.IsSuccess = false;
            _response.Message = "User does not exist.";
            return NotFound(_response);
        }

        existingUser.Name = userData.Name;
        existingUser.DateOfBirth = userData.DateOfBirth;
        existingUser.Gender = userData.Gender;
        existingUser.Email = userData.Email;
        existingUser.UserName = userData.UserName;
        existingUser.PhoneNumber = userData.PhoneNumber;
        var result = await _userManager.UpdateAsync(existingUser);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        _response.Message = "User profile updated successfully.";
        _response.Result = existingUser.MapToDto();
        return Ok(_response);
    }

    /// <summary>
    /// Get user addresses
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet("GetUserAddresses/{userid}")]
    public async Task<IActionResult> GetUserAddresses(string userId)
    {
        var addresses = await _userRepository.GetuserAddressesAsync(userId);
        _response.Result = addresses;
        return Ok(_response);

    }

    [HttpPost("CreateAddress")]
    [ValidateModel]
    public async Task<ActionResult<ResponseDto>> CreateAddress(AddressModel addressInfo)
    {
        var isAddressCreated = await _userRepository.CreateAddressAsync(addressInfo);
        if (!isAddressCreated)
        {
            _response.IsSuccess = false;
            _response.Message = "Address not created, contact to admin.";
            return BadRequest(_response);
        }
        _response.Result = isAddressCreated;
        return Ok(_response);
    }

    /// <summary>
    /// Delete address
    /// </summary>
    /// <param name="addressId"></param>
    /// <returns></returns>
    [HttpDelete("DeleteAddress/{addressId}")]
    public async Task<ActionResult> DeleteAddress(int addressId)
    {
        var isAddressDeleted = await _userRepository.DeleteAddressAsync(addressId);
        if (!isAddressDeleted)
        {
            _response.IsSuccess = false;
            _response.Message = "Address not delete, contact to admin.";
            return BadRequest(_response);
        }
        _response.Result = isAddressDeleted;
        return Ok(_response);
    }
}
