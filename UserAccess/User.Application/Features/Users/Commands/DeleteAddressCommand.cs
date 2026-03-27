using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Users.Commands
{
    public class DeleteAddressCommand : IRequest<ResponseDto>
    {
        public int AddressId { get; set; }
    }
}