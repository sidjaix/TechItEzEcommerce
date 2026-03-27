using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;
using UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class UpdateRoleCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<UpdateRoleCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var hasUpdated = await identityRepository.UpdateRoleAsync(request.Role);

            if (hasUpdated)
            {
                response.Result = request.Role;
                response.Message = "Role updated successfully";
            }
            else
            {
                response.Message = "Failed to update role.";
                response.IsSuccess = false;
            }

            return response;
        }
    }
}