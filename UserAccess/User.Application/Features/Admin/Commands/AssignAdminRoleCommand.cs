using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Commands
{
    public class AssignAdminRoleCommand : IRequest<ResponseDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}