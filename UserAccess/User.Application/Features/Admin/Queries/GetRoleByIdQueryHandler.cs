using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserAccess.Core.Entities;
using UserAccess.Application.Dtos;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetRoleByIdQueryHandler(IIdentityRepository identityRepository) : IRequestHandler<GetRoleByIdQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var role = await identityRepository.GetRoleByIdAsync(request.RoleId);
            if (role is null)
            {
                response.Message = "Role does not exist.";
                response.IsSuccess = false;
                return response;
            }
            response.Result = role;
            return response;
        }
    }
}