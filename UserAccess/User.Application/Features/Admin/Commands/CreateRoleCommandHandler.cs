using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User_Core.Entities;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ResponseDto>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(RoleManager<Role> roleManager, ILogger<CreateRoleCommandHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var role = request.Role;

            _logger.LogInformation("Creating a new role with name {RoleName}", role.RoleName);
            var existingRole = await _roleManager.FindByIdAsync(role.RoleId);
            if (existingRole != null)
            {
                _logger.LogWarning("Role {RoleName} Already exists", role.RoleName);
                response.Message = "Role already exist";
                response.IsSuccess = false;
                return response;
            }

            var result = await _roleManager.CreateAsync(role.MapToEntity());
            if (!result.Succeeded)
            {
                _logger.LogWarning("Role has not created {@Errors}", result.Errors);
                response.Message = "Role has not created.";
                response.IsSuccess = false;
                return response;
            }

            response.Message = "Role created successfully.";
            return response;
        }
    }
}