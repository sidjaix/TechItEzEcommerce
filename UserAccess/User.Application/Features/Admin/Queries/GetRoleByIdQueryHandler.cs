using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User_Core.Entities;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.User;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, ResponseDto>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;

        public GetRoleByIdQueryHandler(RoleManager<Role> roleManager, ILogger<GetRoleByIdQueryHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            try
            {
                _logger.LogInformation("Getting role by id {RoleId}", request.RoleId);
                var role = await _roleManager.FindByIdAsync(request.RoleId);
                if (role is null)
                {
                    _logger.LogWarning("Role with id {RoleId} not found", request.RoleId);
                    response.Message = "Role does not exist.";
                    response.IsSuccess = false;
                    return response;
                }
                response.Result = new RoleModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name!,
                    Description = role.Description!
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting role by id {RoleId}", request.RoleId);
                response.Message = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}