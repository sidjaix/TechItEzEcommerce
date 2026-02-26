using MediatR;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.Auth;

namespace UserAccess.Application.Features.Auth.Commands;

public class RegisterCommand : IRequest<ResponseDto>
{
    public RegisterModel Register { get; }

    public RegisterCommand(RegisterModel register)
    {
        Register = register;
    }
}