using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Core.Entities;
using UserAccess.Application.Mappers;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetRolesQueryHandler(IIdentityRepository identityRepository) : IRequestHandler<GetRolesQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();

            var roles = await identityRepository.GetRolesAsync(cancellationToken);
            response.Result = roles;

            return response;
        }
    }
}