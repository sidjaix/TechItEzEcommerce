using MediatR;
using Microsoft.Extensions.Options;
using UserAccess.Application.Dtos;
using User.Application.Interfaces;

namespace UserAccess.Application.Features.Auth.Commands;

public class LoginCommandHandler(IIdentityRepository identityRepository, IOptions<JwtOptions> jwtOptions) : IRequestHandler<LoginCommand, LoginResponseModel>
{
    public async Task<LoginResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = new LoginResponseModel();
        bool isValid = await identityRepository.ValidateUserPasswordAsync(request.UserName, request.Password);
        if (!isValid)
        {
            response.User = default;
            response.Token = string.Empty;
            return response;
        }

        var user = await identityRepository.GetUserRolesAsync(request.UserName);
        var token = identityRepository.GenerateToken(user, jwtOptions.Value);

        response.User = user;
        response.Token = token;
        return response;
    }
}