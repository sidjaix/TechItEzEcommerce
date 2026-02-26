using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.User.Commands
{
    public class DeleteAddressCommand : IRequest<ResponseDto>
    {
        public int AddressId { get; set; }
    }
}