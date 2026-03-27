using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Features.Admin.Queries;
using UserAccess.Application.Features.Users.Queries;

namespace User_AzureFunctions
{
    public class User
    {
        private readonly ILogger<User> _logger;
        private readonly IMediator _mediator;

        public User(ILogger<User> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Function("GetUsers")]
        public async Task<IActionResult> GetUsers([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = await _mediator.Send(new GetUsersQuery());
            if (response.IsSuccess)
            {
                return new OkObjectResult(response.Result);
            }
            return new BadRequestObjectResult(response.Message);
        }

        [Function("GetUser")]
        public async Task<IActionResult> GetUser([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("HTTP trigger GetUser function processed a request.");
            var userIdQuery = req.Query["userId"];
            if (string.IsNullOrEmpty(userIdQuery))
            {
                return new BadRequestObjectResult("Please provide a userId in the query string.");
            }

            var response = await _mediator.Send(new GetUserByIdQuery { UserId = userIdQuery! });
            if (response.IsSuccess)
            {
                return new OkObjectResult(response.Result);
            }
            return new BadRequestObjectResult(response.Message);
        }
    }
}
