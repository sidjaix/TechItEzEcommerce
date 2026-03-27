using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Users.Commands
{
    public class UpdateUserCommand : IRequest<ResponseDto>
    {
        public UserModel User { get; set; } = new();
    }
}