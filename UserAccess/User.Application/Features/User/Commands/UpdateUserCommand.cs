using MediatR;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.User;

namespace UserAccess.Application.Features.User.Commands
{
    public class UpdateUserCommand : IRequest<ResponseDto>
    {
        public UserModel User { get; set; } = new();
    }
}