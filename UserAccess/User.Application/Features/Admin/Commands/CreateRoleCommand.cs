using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class CreateRoleCommand : IRequest<ResponseDto>
    {
        public RoleModel Role { get; set; } = new();
    }
}