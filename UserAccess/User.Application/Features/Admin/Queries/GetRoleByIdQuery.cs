using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetRoleByIdQuery : IRequest<ResponseDto>
    {
        public string RoleId { get; set; } = string.Empty;
    }
}