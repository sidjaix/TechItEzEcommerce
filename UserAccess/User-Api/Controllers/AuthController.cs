using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAccess.Application.Dtos;
using UserAccess.Application.Features.Auth.Commands;

namespace UserAccess.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController(IMediator mediator, ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginAsync([FromBody] LoginCommand command)
        {
            var loginResponse = await mediator.Send(command);
            var response = new ResponseDto();
            if (loginResponse.User is null || string.IsNullOrEmpty(loginResponse.Token))
            {
                logger.LogWarning("User with email {UserName} failed to login", command.UserName);
                response.Message = "Username or password is incorrect.";
                response.IsSuccess = false;
                return BadRequest(response);
            }
            response.Result = loginResponse;
            response.Message = "User has logged in successfully";
            return Ok(response);
        }

        [HttpPost("register")]
        //[ValidateModel] // This will be replaced by FluentValidation pipeline
        public async Task<ActionResult<ResponseDto>> RegisterAsync([FromBody] RegisterCommand command)
        {
            logger.LogInformation("A new user with email {Email} is attempting to register", command.Register.Email);

            var response = await mediator.Send(command);

            if (!response.IsSuccess)
            {
                logger.LogWarning("User with email {Email} failed to register: {Message}", command.Register.Email, response.Message);
                return BadRequest(response);
            }

            logger.LogInformation("User with email {Email} has registered successfully", command.Register.Email);
            return Ok(response);
        }

    }
}
