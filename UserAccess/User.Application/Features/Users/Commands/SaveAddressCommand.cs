using MediatR;
using UserAccess.Application.Dtos;

namespace UserAccess.Application.Features.Users.Commands
{
    public class SaveAddressCommand : IRequest<ResponseDto>
    {
        public AddressModel Address { get; set; } = new();
    }
}