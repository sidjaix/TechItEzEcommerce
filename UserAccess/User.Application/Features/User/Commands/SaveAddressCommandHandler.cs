using MediatR;
using Microsoft.Extensions.Logging;
using User_Core.Interfaces;
using UserAccess.Application.Dtos;
using UserAccess.Application.Mappers;

namespace UserAccess.Application.Features.User.Commands
{
    public class SaveAddressCommandHandler : IRequestHandler<SaveAddressCommand, ResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<SaveAddressCommandHandler> _logger;

        public SaveAddressCommandHandler(IUserRepository userRepository, ILogger<SaveAddressCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(SaveAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            var addressInfo = request.Address;
            bool isSuccess;

            if (addressInfo.AddressId == 0)
            {
                _logger.LogInformation("Attempting to create a new address");
                isSuccess = await _userRepository.CreateAddressAsync(addressInfo.MapToEntity());
                if (!isSuccess)
                {
                    _logger.LogError("Failed to create address");
                    response.Message = "Address not created, contact to admin.";
                }
            }
            else
            {
                _logger.LogInformation("Attempting to update address {AddressId}", addressInfo.AddressId);
                isSuccess = await _userRepository.UpdateAddressAsync(addressInfo.MapToEntity());
                if (isSuccess)
                {
                    _logger.LogInformation("Address {AddressId} updated successfully", addressInfo.AddressId);
                    response.Message = "Address updated successfully.";
                }
                else
                {
                    _logger.LogError("Failed to update address {AddressId}", addressInfo.AddressId);
                    response.Message = "Address not updated, contact to admin.";
                }
            }

            response.IsSuccess = isSuccess;
            response.Result = isSuccess;

            return response;
        }
    }
}