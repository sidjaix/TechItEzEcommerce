using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Entities = UserAccess.Core.Entities;
using UserAccess.Application.Dtos;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class AssignRoleCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<AssignRoleCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var hasAssigned = await identityRepository.AssignRoleAsync(request.Email, request.RoleName);
            if (!hasAssigned)
            {
                response.IsSuccess = false;
                response.Message = "Failed to assign role.";
                return response;
            }
            response.Message = "User has been assigned to the role.";
            return response;
        }
    }
}