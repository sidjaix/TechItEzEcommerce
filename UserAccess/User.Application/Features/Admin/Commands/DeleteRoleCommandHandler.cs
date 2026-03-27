using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ResponseDto>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        public DeleteRoleCommandHandler(RoleManager<Role> roleManager, ILogger<DeleteRoleCommandHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Deleting role {RoleId}", request.RoleId);

            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role is null)
            {
                _logger.LogWarning("Role {RoleId} not found", request.RoleId);
                response.Message = "Role does not exist";
                response.IsSuccess = false;
                return response;
            }
            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to delete role {RoleId}: {Errors}", request.RoleId, result.Errors);
                response.IsSuccess = false;
                response.Message = "Role has not been deleted.";
                return response;
            }
            response.Result = true;
            response.Message = "Role deleted successfully.";
            return response;
        }
    }
}