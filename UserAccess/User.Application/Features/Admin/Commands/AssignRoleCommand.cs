using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class AssignRoleCommand : IRequest<ResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}