using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ResponseDto>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;

        public UpdateRoleCommandHandler(RoleManager<Role> roleManager, ILogger<UpdateRoleCommandHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var roleData = request.Role;

            try
            {
                _logger.LogInformation("Updating role {RoleId}", roleData.RoleId);
                var role = await _roleManager.FindByIdAsync(roleData.RoleId);
                if (role == null)
                {
                    _logger.LogWarning("Role {RoleId} not found", roleData.RoleId);
                    response.Message = "Role does not exist";
                    response.IsSuccess = false;
                    return response;
                }

                role.Name = roleData.RoleName;
                role.NormalizedName = roleData.RoleName.ToUpper();
                role.Description = roleData.Description;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    response.Result = role;
                    response.Message = "Role updated successfully";
                }
                else
                {
                    _logger.LogError("Failed to update role {RoleId}: {Errors}", roleData.RoleId, result.Errors);
                    response.Message = "Failed to update role.";
                    response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating role {RoleId}", roleData.RoleId);
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}