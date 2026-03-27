using MediatR;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Application.Interfaces;

namespace UserAccess.Application.Features.Users.Commands
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DeleteAddressCommandHandler> _logger;

        public DeleteAddressCommandHandler(IUserRepository userRepository, ILogger<DeleteAddressCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Attempting to delete address {AddressId}", request.AddressId);

            var isAddressDeleted = await _userRepository.DeleteAddressAsync(request.AddressId);
            if (!isAddressDeleted)
            {
                _logger.LogError("Failed to delete address {AddressId}", request.AddressId);
                response.IsSuccess = false;
                response.Message = "Address not deleted, contact to admin.";
                return response;
            }

            _logger.LogInformation("Address {AddressId} deleted successfully", request.AddressId);
            response.Result = isAddressDeleted;
            return response;
        }
    }
}