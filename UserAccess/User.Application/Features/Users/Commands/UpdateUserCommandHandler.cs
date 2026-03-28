using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;
using Entity = UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Users.Commands
{
    public class UpdateUserCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<UpdateUserCommand, ResponseDto>
    {
        public async Task<ResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userData = request.User;
            var result = await identityRepository.UpdateUserAsync(userData);
            return result;
        }
    }
}