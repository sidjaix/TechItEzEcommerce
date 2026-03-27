using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}