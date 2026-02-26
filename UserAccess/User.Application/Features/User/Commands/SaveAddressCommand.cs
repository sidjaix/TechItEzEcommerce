using MediatR;
using UserAccess.Application.Dtos;
using UserAccess.Application.Dtos.Address;

namespace UserAccess.Application.Features.User.Commands
{
    public class SaveAddressCommand : IRequest<ResponseDto>
    {
        public AddressModel Address { get; set; } = new();
    }
}