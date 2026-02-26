using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Admin.Queries
{
    public class GetRolesQuery : IRequest<ResponseDto>
    {
    }
}