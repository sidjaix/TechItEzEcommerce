using MediatR;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Dtos;
using UserAccess.Application.Interfaces;

namespace UserAccess.Application.Features.Users.Queries
{
    public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, ResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetAddressQueryHandler> _logger;

        public GetAddressQueryHandler(IUserRepository userRepository, ILogger<GetAddressQueryHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ResponseDto> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            var response = new ResponseDto();
            _logger.LogInformation("Attempting to get address {AddressId}", request.AddressId);

            var address = await _userRepository.GetAddressAsync(request.AddressId);
            if (address is null)
            {
                _logger.LogWarning("Address {AddressId} not found", request.AddressId);
                response.Message = "Address not found";
                response.IsSuccess = false;
                return response;
            }

            response.Result = address;
            return response;
        }
    }
}