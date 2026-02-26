using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.User.Queries
{
    public class GetUserByIdQuery : IRequest<ResponseDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}