using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserAccess.Core.Entities;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class AssignAdminRoleCommandHandler : IRequestHandler<AssignAdminRoleCommand, ResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AssignAdminRoleCommandHandler> _logger;

        public AssignAdminRoleCommandHandler(UserManager<User> userManager, ILogger<AssignAdminRoleCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Assigning admin role to user {UserId}", request.UserId);

            var existingUser = await _userManager.FindByIdAsync(request.UserId);
            if (existingUser is null)
            {
                _logger.LogWarning("User {UserId} not found", request.UserId);
                response.Message = "User does not exist.";
                response.IsSuccess = false;
                return response;
            }
            var isAdmin = await _userManager.IsInRoleAsync(existingUser, RoleStore.ADMIN);
            if (isAdmin)
            {
                response.Message = "User is already an admin.";
                response.IsSuccess = false;
                return response;
            }
            var result = await _userManager.AddToRoleAsync(existingUser, RoleStore.ADMIN);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to assign admin role to user {UserId}: {Errors}", request.UserId, result.Errors);
                response.IsSuccess = false;
                response.Message = "Failed to assign admin role.";
                return response;
            }
            response.Message = "User has been assigned to the admin role.";
            return response;
        }
    }
}