using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Users.Queries
{
    public class GetAddressQuery : IRequest<ResponseDto>
    {
        public int AddressId { get; set; }
    }
}