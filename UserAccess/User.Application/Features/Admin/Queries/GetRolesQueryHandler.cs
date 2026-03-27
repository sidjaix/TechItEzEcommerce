using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Core.Entities;
using UserAccess.Application.Mappers;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, ResponseDto>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<GetRolesQueryHandler> _logger;

        public GetRolesQueryHandler(RoleManager<Role> roleManager, ILogger<GetRolesQueryHandler> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            try
            {
                _logger.LogInformation("Getting all roles");
                var roles = await _roleManager.Roles.AsNoTracking().Select(x => x.MapToDto()).ToListAsync(cancellationToken);
                response.Result = roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all roles");
                response.Message = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
    }
}