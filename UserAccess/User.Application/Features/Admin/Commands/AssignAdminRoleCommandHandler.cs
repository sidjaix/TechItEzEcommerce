using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Entity = UserAccess.Core.Entities;
using UserAccess.Application.Dtos;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class AssignAdminRoleCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<AssignAdminRoleCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AssignAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var hasAssigned = await identityRepository.AssignAdminRoleAsync(request.UserId);
            if (!hasAssigned)
            {
                response.IsSuccess = false;
                response.Message = "Failed to assign admin role.";
                return response;
            }
            response.Message = "User has been assigned to the admin role.";
            return response;
        }
    }
}