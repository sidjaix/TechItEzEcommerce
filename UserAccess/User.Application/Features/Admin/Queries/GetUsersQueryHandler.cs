using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;
using UserAccess.Application.Interfaces;
using UserAccess.Application.Mappers;
using Entity = UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, ResponseDto>
    {
        public async Task<ResponseDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var users = await userRepository.GetUsersAsync();
            response.Result = users;
            return response;
        }
    }
}