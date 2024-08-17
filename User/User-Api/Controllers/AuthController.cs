using Microsoft.AspNetCore.Mvc;
using User_Api.Services.IServices;
using User_Api.Common.Filters;
using User_Core.Models;

namespace User_Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService, ResponseDto response) : ControllerBase
    {
        [HttpPost("login")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> LoginAsync([FromBody] LoginModel loginModel)
        {
            var loginResponse = await authService.LoginAsync(loginModel);
            if (loginResponse.User is null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.Message = "Username or password is incorrect.";
                response.IsSuccess = false;
                response.Result = default;
                return BadRequest(response);
            }
            response.Result = loginResponse;
            response.Message = "User has logged in successfully";
            return Ok(response);
        }

        [HttpPost("register")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> RegisterAsync([FromBody] RegisterModel register)
        {
            var response = await authService.RegisterAsync(register);
            if (!response.IsSuccess)
            {
                response.Message = "User has not registered.";
                response.IsSuccess = false;
                response.Result = default;
                return BadRequest(response);
            }
            response.Result = response;
            return Ok(response);
        }

    }
}
