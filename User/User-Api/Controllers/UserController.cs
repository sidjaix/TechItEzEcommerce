using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Api.Common.Filters;
using User_Core.Entities;
using User_Core.Models;
using User_Data.Repository.IRepository;

namespace User_Api.Controllers;


[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly ResponseDto _response;

    public UserController(IUserRepository userRepository, UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _response = new ResponseDto();
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
        _response.Result = existingUser;
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
            _response.Message = $"User has not updated.";
            _response.IsSuccess = false;
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }
        _response.Result = existingUser;
        return Ok(existingUser);
    }

}
