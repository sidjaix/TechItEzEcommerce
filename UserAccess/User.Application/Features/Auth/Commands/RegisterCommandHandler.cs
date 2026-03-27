using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Interfaces;
using UserAccess.Application.Dtos;
using Entity = UserAccess.Core.Entities;

namespace UserAccess.Application.Features.Auth.Commands;

public class RegisterCommandHandler(IIdentityRepository identityRepository) : IRequestHandler<RegisterCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = request.Register;
        var result = await identityRepository.CreateUserAsync(newUser);
        if (!result.IsSuccess)
        {
            return result;
        }

        // Add a default 'USER' role to all new user
        var hasRoleAssigned = await identityRepository.AssignRoleAsync(newUser.Email, RoleStore.USER);
        if (!hasRoleAssigned)
        {
            return new ResponseDto()
            {
                IsSuccess = false, // Or true, depending on desired outcome. Let's mark it as failure.
                Message = $"User created, but failed to assign role '{RoleStore.USER}'. Please contact support."
            };
        }

        return new ResponseDto()
        {
            IsSuccess = true,
            Message = "User registered successfully."
        };
    }
}