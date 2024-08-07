using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using User_Data.Interface;

namespace User_AzureFunctions
{
    public class User
    {
        private readonly ILogger<User> _logger;
        private readonly ICustomerRepository _customerRepository;

        public User(ILogger<User> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [Function("GetUsers")]
        public IActionResult GetUsers([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var users = _customerRepository.GetUsers();
            return new OkObjectResult(users);
        }

        [Function("GetUser")]
        public IActionResult GetUser([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("HTTP trigger GetUser function processed a request.");
            var userIdQuery = req.Query["userId"];
            if (string.IsNullOrEmpty(userIdQuery))
            {
                return new BadRequestObjectResult("Please provide a userId in the query string.");
            }
            if (!int.TryParse(userIdQuery, out int userId))
            {
                return new BadRequestObjectResult("Invalid User ID format.");
            }

            var user = _customerRepository.GetUserById(userId);
            return new OkObjectResult(user);
        }
    }
}
