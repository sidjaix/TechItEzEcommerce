using MediatR;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.User;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class CreateRoleCommand : IRequest<ResponseDto>
    {
        public RoleModel Role { get; set; } = new();
    }
}