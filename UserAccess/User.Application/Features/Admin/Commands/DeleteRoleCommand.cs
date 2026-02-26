using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class DeleteRoleCommand : IRequest<ResponseDto>
    {
        public string RoleId { get; set; } = string.Empty;
    }
}