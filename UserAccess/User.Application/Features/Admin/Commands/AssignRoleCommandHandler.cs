using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Entities = UserAccess.Core.Entities;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, ResponseDto>
    {
        private readonly UserManager<Entities.User> _userManager;
        private readonly ILogger<AssignRoleCommandHandler> _logger;

        public AssignRoleCommandHandler(UserManager<Entities.User> userManager, ILogger<AssignRoleCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Assigning role {RoleName} to user {Email}", request.RoleName, request.Email);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser is null)
            {
                _logger.LogWarning("User with email {Email} not found", request.Email);
                response.Message = "User does not exist.";
                response.IsSuccess = false;
                return response;
            }
            var result = await _userManager.AddToRoleAsync(existingUser, request.RoleName);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to assign role {RoleName} to user {Email}: {Errors}", request.RoleName, request.Email, result.Errors);
                response.IsSuccess = false;
                response.Message = "Failed to assign role.";
                return response;
            }
            response.Message = "User has been assigned to the role.";
            return response;
        }
    }
}