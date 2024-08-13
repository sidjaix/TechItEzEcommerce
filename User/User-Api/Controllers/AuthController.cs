using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User_Api.Services.IServices;
using User_Api.Common.Filters;
using User_Core.Entities;
using User_Core.Models;

namespace User_Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> LoginAsync([FromBody] LoginModel loginModel)
        {
            var response = await _authService.LoginAsync(loginModel);
            if (response.User is null || string.IsNullOrEmpty(response.Token))
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        [ValidateModel]
        public async Task<ActionResult<ResponseDto>> RegisterAsync([FromBody] RegisterModel register)
        {
            var response = await _authService.RegisterAsync(register);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
