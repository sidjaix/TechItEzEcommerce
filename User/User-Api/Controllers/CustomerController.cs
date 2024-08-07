using Microsoft.AspNetCore.Mvc;
using User_Api.Common.Filters;
using User_Core.Models;
using User_Data.Interface;

namespace User_Api.Controllers;


[ApiController]
[Route("api/user")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository customerRepository;

    public CustomerController(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    /// <summary>
    /// Get users/customers of the application
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = customerRepository.GetUsers();
        return Ok(users);
    }

    /// <summary>
    /// Create user/customer in the application
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns>User</returns>
    [HttpPost]
    [ValidateModel]
    public ActionResult<UserModel> CreateUser([FromBody] UserModel userModel)
    {
        var createdUserProfile = customerRepository.CreateUser(userModel);
        return CreatedAtAction(nameof(GetUserById), new { userId = createdUserProfile.UserId }, createdUserProfile);
    }

    /// <summary>
    /// Get user/customer data
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>UserModel</returns>
    [HttpGet("{userId}")]
    public ActionResult<UserModel> GetUserById(int userId)
    {
        var user = customerRepository.GetUserById(userId);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Update user/customer info
    /// </summary>
    /// <param name="userData"></param>
    /// <returns>UserModel</returns>
    [HttpPut]
    public ActionResult<UserModel> UpdateUser(UserModel userData)
    {
        var user = customerRepository.UpdateUser(userData);
        return Ok(user);
    }

}
