using MediatR;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.Auth;

namespace UserAccess.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<LoginResponseModel>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}