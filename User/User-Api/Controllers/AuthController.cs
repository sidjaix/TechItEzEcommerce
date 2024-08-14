using Microsoft.AspNetCore.Mvc;
using User_Api.Services.IServices;
using User_Api.Common.Filters;
using User_Core.Models;

namespace User_Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;
        public AuthController(IAuthService authService, ResponseDto response)
        {
            _authService = authService;
            _response = response;

        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> LoginAsync([FromBody] LoginModel loginModel)
        {
            var response = await _authService.LoginAsync(loginModel);
            if (response.User is null || string.IsNullOrEmpty(response.Token))
            {
                _response.Message = "Username or password is incorrect.";
                _response.IsSuccess = false;
                _response.Result = default;
                return BadRequest(_response);
            }
            _response.Result = response;
            _response.Message = "User has logged in successfully";
            return Ok(_response);
        }

        [HttpPost("register")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> RegisterAsync([FromBody] RegisterModel register)
        {
            var response = await _authService.RegisterAsync(register);
            if (!response.IsSuccess)
            {
                _response.Message = "User has not registered.";
                _response.IsSuccess = false;
                _response.Result = default;
                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(response);
        }

    }
}
